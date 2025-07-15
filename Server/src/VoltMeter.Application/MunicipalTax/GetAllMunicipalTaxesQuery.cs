using System.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VoltMeter.Application.Services;
using VoltMeter.Domain.ConsumptionRecord;
using VoltMeter.Domain.Meter;
using VoltMeter.Domain.MonthlyRate;
using VoltMeter.Domain.Municipal;
using VoltMeter.Domain.PtfRate;
using VoltMeter.Shared;

namespace VoltMeter.Application.MunicipalTax;

public sealed record GetAllMunicipalTaxesQuery() : IRequest<BaseResult<IEnumerable<GetAllMunicipalTaxesResponse>>>;

public sealed record GetAllMunicipalTaxesResponse
{
    public string MunicipalName { get; set; } = default!;
    public string MeterNo { get; set; } = default!;
    public int Tariff { get; set; }
    public string TariffName => Tariff == (int)TariffType.Industrial ? "Sanayi" : "Ticarethane";
    public string InvoicePeriod { get; set; } = default!;
    public decimal LocalTax { get; set; }
    public string LocalTaxDisplay => $"%{(int)(LocalTax * 100)}";
    public decimal TotalAmount { get; set; }
    public decimal TotalLocalTax => TotalAmount * LocalTax;
}

internal sealed class GetAllMunicipalTaxesQueryHandler(IMunicipalRepository _municipalRepository,
                                                       IMeterRepository _meterRepository,
                                                       IConsumptionRecordRepository _consumptionRecordRepository,
                                                       IPtfRateRepository _ptfRateRepository,
                                                       IMonthlyRateRepository _monthlyRateRepository,
                                                       InvoiceDataCalculationService _invoiceDataCalculationService) : IRequestHandler<GetAllMunicipalTaxesQuery, BaseResult<IEnumerable<GetAllMunicipalTaxesResponse>>>
{
    public async Task<BaseResult<IEnumerable<GetAllMunicipalTaxesResponse>>> Handle(GetAllMunicipalTaxesQuery request, CancellationToken cancellationToken)
    {
        var municipalList = await _municipalRepository.GetAll().ToDictionaryAsync(dic => dic.Id, cancellationToken);
        var meterList = await _meterRepository.GetAll().ToDictionaryAsync(dic => dic.Id, cancellationToken);
        var consumptionList = await _consumptionRecordRepository.GetAll().ToListAsync(cancellationToken);
        var monthlyRateValues = await _monthlyRateRepository.GetAll().FirstOrDefaultAsync(cancellationToken);
        
        if (monthlyRateValues is null)
            return new BaseResult<IEnumerable<GetAllMunicipalTaxesResponse>>((int)HttpStatusCode.NotFound, "Tarife Bilgisi Bulunamadı!");

        var ptfRateList = await _ptfRateRepository.GetAll().ToDictionaryAsync(dic => (dic.Period.Day, dic.Period.Hour), dic => dic.Price, cancellationToken);

        var result = _invoiceDataCalculationService.Calculate(meterList, consumptionList, monthlyRateValues, ptfRateList);
        if (result is null || !result.Any())
            return new BaseResult<IEnumerable<GetAllMunicipalTaxesResponse>>((int)HttpStatusCode.NotFound, "Fatura Bilgisi Bulunamadı!");

        var responseData = result.Select(s => new GetAllMunicipalTaxesResponse
        {
            MeterNo = s.MeterNo,
            InvoicePeriod = $"{s.Year} - {s.Month}",
            Tariff = s.Tariff,
            MunicipalName = municipalList[meterList[s.MeterId].MunicipalId].Name,
            LocalTax = municipalList[meterList[s.MeterId].MunicipalId].LocalTax,
            TotalAmount = s.TotalAmount
        }).ToList().OrderBy(o => o.MeterNo);

        return new BaseResult<IEnumerable<GetAllMunicipalTaxesResponse>>(responseData);
    }
}