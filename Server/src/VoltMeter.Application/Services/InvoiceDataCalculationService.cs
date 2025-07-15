using VoltMeter.Application.Contracts;
using VoltMeter.Domain.ConsumptionRecord;
using VoltMeter.Domain.Meter;
using VoltMeter.Domain.MonthlyRate;
using VoltMeter.Shared;

namespace VoltMeter.Application.Services;

public class InvoiceDataCalculationService
{
    public IEnumerable<InvoiceResultDto>? Calculate(Dictionary<Guid, Meter> meterList,
        List<ConsumptionRecord> consumptions,
        MonthlyRate monthlyRate,
        Dictionary<(int Day, int Hour), decimal> ptfRateList)
    {
        var results = consumptions.GroupBy(x => new { x.MeterId, x.ReadingDay.Year, x.ReadingDay.Month })
            .Select(g =>
            {
                meterList.TryGetValue(g.Key.MeterId, out var meter);
                if (meter is null)
                    throw new ArgumentException($"Sayaç Bulunamadı: {g.Key.MeterId}");
                var totalConsumption = g.Sum(s => s.ReadingValue);

                var totalPtf = meter.SalesMethod != (int)SalesMethodType.TariffDiscounted ?
                                g.Sum(s => s.ReadingValue * (ptfRateList.TryGetValue((s.ReadingDay.Day, s.ReadingHour % 24), out var p) ? p : 0m)) : 0m;

                var totalYek = meter.SalesMethod != (int)SalesMethodType.TariffDiscounted ? totalConsumption * monthlyRate.YekPrice : 0m;

                var totalEnergyTariff = meter.SalesMethod == (int)SalesMethodType.TariffDiscounted
                    ? totalConsumption * (meter.Tariff == (int)TariffType.Industrial
                        ? monthlyRate.IndustrialEnergyTariff
                        : monthlyRate.CommercialEnergyTariff)
                    : 0m;

                var totalDistributionTariff = meter.SalesMethod == (int)SalesMethodType.TariffDiscounted
                    ? totalConsumption * (meter.Tariff == (int)TariffType.Industrial
                        ? monthlyRate.IndustrialDistributionTariff
                        : monthlyRate.CommercialDistributionTariff)
                    : 0m;

                var totalAmount = meter.SalesMethod switch
                {
                    (int)SalesMethodType.PtfYekMultipliedByCommission => totalPtf + totalYek + ((totalPtf + totalYek) * (meter.Commission ?? 0m)),
                    (int)SalesMethodType.PtfYekAddedWithCommission => totalPtf + totalYek + (meter.Commission ?? 0m),
                    (int)SalesMethodType.TariffDiscounted => totalEnergyTariff - (totalEnergyTariff * (meter.Discount ?? 0)) + totalDistributionTariff,
                    _ => throw new ArgumentException("Tanımlanmayan Satış Yöntemi")
                };

                return new InvoiceResultDto()
                {
                    MeterId = meter.Id,
                    MeterNo = meter.No,
                    Tariff = meter.Tariff,
                    Vat = meter.Vat,
                    Discount = meter.Discount,
                    Commission = meter.Commission,
                    SalesMethod = meter.SalesMethod,
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalConsumption = totalConsumption,
                    TotalPtfAmount = totalPtf,
                    TotalYekAmount = totalYek,
                    TotalEnergyTariffAmount = totalEnergyTariff,
                    TotalDistributionTariffAmount = totalDistributionTariff,
                    TotalAmount = totalAmount
                };
            }).ToList();

        return results;
    }
}