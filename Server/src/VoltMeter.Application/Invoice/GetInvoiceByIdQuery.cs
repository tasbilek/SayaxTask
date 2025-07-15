using System.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VoltMeter.Application.Services;
using VoltMeter.Domain.ConsumptionRecord;
using VoltMeter.Domain.Meter;
using VoltMeter.Domain.MonthlyRate;
using VoltMeter.Domain.PtfRate;
using VoltMeter.Shared;

namespace VoltMeter.Application.Invoice;

public sealed record GetInvoiceByIdQuery(string MeterId) : IRequest<BaseResult<GetInvoiceByIdResponse>>;

public sealed record GetInvoiceByIdResponse
{
    public Guid MeterId { get; set; }
    public string MeterNo { get; set; } = default!;
    public string Period { get; set; } = default!;
    public int Tariff { get; set; }
    public string TariffName => Tariff == (int)TariffType.Industrial ? "Sanayi" : "Ticarethane";
    public int SalesMethod { get; set; }
    public decimal? Commission { get; set; }
    public string CommissionDisplay => !Commission.HasValue ? "-" : (Commission > 1 ? $"{(int)Commission}" : $"%{(int)(Commission * 100)}");
    public decimal? Discount { get; set; }
    public string DiscountDisplay => !Discount.HasValue ? "-" : (Discount > 1 ? $"{(int)Discount}" : $"%{(int)(Discount * 100)}");
    public decimal TotalConsumption { get; set; }
    public decimal TotalPtfAmount { get; set; }
    public decimal TotalYekAmount { get; set; }
    public decimal TotalEnergyTariffAmount { get; set; }
    public decimal TotalDistributionTariffAmount { get; set; }
    public decimal Vat { get; set; }
    public string VatDisplay => $"%{(int)(Vat * 100)}";
    public decimal TotalAmount { get; set; }
    public decimal TotalAmountWithVat => TotalAmount + (TotalAmount * Vat);
}

internal sealed class GetInvoiceByIdQueryHandler(IMeterRepository _meterRepository,
                                                 IConsumptionRecordRepository _consumptionRecordRepository,
                                                 IPtfRateRepository _ptfRateRepository,
                                                 IMonthlyRateRepository _monthlyRateRepository,
                                                 InvoiceDataCalculationService _invoiceDataCalculationService) : IRequestHandler<GetInvoiceByIdQuery, BaseResult<GetInvoiceByIdResponse>>
{
    public async Task<BaseResult<GetInvoiceByIdResponse>> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
    {
        Guid meterId = Guid.Parse(request.MeterId);
        var meters = await _meterRepository.Where(f => f.Id == meterId).ToDictionaryAsync(dic => dic.Id, cancellationToken);

        if (meters is null || meters.Count == 0)
            return new BaseResult<GetInvoiceByIdResponse>((int)HttpStatusCode.NotFound, "Sayaç Bilgisi Bulunamadı!");

        var consumptionList = await _consumptionRecordRepository.Where(w => w.MeterId == meterId).ToListAsync(cancellationToken);
        var monthlyRateValues = await _monthlyRateRepository.GetAll().FirstOrDefaultAsync(cancellationToken);

        if (monthlyRateValues is null)
            return new BaseResult<GetInvoiceByIdResponse>((int)HttpStatusCode.NotFound, "Tarife Bilgisi Bulunamadı!");

        var ptfRateList = await _ptfRateRepository.GetAll().ToDictionaryAsync(dic => (dic.Period.Day, dic.Period.Hour), dic => dic.Price, cancellationToken);

        var result = _invoiceDataCalculationService.Calculate(meters, consumptionList, monthlyRateValues, ptfRateList);
        if (result is null || !result.Any())
            return new BaseResult<GetInvoiceByIdResponse>((int)HttpStatusCode.NotFound, "Fatura Bilgisi Bulunamadı!");

        var responseData = result.Select(s => new GetInvoiceByIdResponse()
        {
            MeterId = s.MeterId,
            MeterNo = s.MeterNo,
            Period = $"{s.Year} - {s.Month}",
            Tariff = s.Tariff,
            SalesMethod = s.SalesMethod,
            Commission = s.Commission,
            Discount = s.Discount,
            TotalConsumption = s.TotalConsumption,
            TotalPtfAmount = s.TotalPtfAmount,
            TotalYekAmount = s.TotalYekAmount,
            TotalEnergyTariffAmount = s.TotalEnergyTariffAmount,
            TotalDistributionTariffAmount = s.TotalDistributionTariffAmount,
            Vat = s.Vat,
            TotalAmount = s.TotalAmount
        }).SingleOrDefault();

        return new BaseResult<GetInvoiceByIdResponse>(responseData!);
    }
}