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

public sealed record GetAllInvoicesQuery: IRequest<BaseResult<IEnumerable<GetAllInvoicesResponse>>>;

public sealed record GetAllInvoicesResponse
{
    public Guid MeterId { get; set; }
    public string MeterNo { get; set; } = default!;
    public string Period { get; set; } = default!;
    public int Tariff { get; set; }
    public string TariffName => Tariff == (int)TariffType.Industrial ? "Sanayi" : "Ticarethane";
    public decimal Vat { get; set; }
    public string VatDisplay => $"%{(int)(Vat * 100)}";
    public decimal TotalAmount { get; set; }
    public decimal TotalAmountWithVat => TotalAmount + (TotalAmount * Vat);
}

internal sealed class GetAllInvoicesQueryHandler(IMeterRepository _meterRepository,
                                                 IConsumptionRecordRepository _consumptionRecordRepository,
                                                 IPtfRateRepository _ptfRateRepository,
                                                 IMonthlyRateRepository _monthlyRateRepository,
                                                 InvoiceDataCalculationService _invoiceDataCalculationService) : IRequestHandler<GetAllInvoicesQuery, BaseResult<IEnumerable<GetAllInvoicesResponse>>>
{
    public async Task<BaseResult<IEnumerable<GetAllInvoicesResponse>>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
    {
        var meterList = await _meterRepository.GetAll().ToDictionaryAsync(dic => dic.Id, cancellationToken);
        var consumptionList = await _consumptionRecordRepository.GetAll().ToListAsync(cancellationToken);
        var monthlyRateValues = await _monthlyRateRepository.GetAll().FirstOrDefaultAsync(cancellationToken);

        if (monthlyRateValues is null)
            return new BaseResult<IEnumerable<GetAllInvoicesResponse>>((int)HttpStatusCode.NotFound, "Tarife Bilgisi Bulunamadı!");

        var ptfRateList = await _ptfRateRepository.GetAll().ToDictionaryAsync(dic => (dic.Period.Day, dic.Period.Hour), dic => dic.Price, cancellationToken);

        var resultList = _invoiceDataCalculationService.Calculate(meterList, consumptionList, monthlyRateValues, ptfRateList);
        if (resultList is null || !resultList.Any())
            return new BaseResult<IEnumerable<GetAllInvoicesResponse>>((int)HttpStatusCode.NotFound, "Fatura Bilgisi Bulunamadı!");

        var responseData = resultList.Select(s => new GetAllInvoicesResponse
        {
            MeterId = s.MeterId,
            MeterNo = s.MeterNo,
            Period = $"{s.Year} - {s.Month}",
            Tariff = s.Tariff,
            Vat = s.Vat,
            TotalAmount = s.TotalAmount
        }).ToList().OrderBy(o => o.MeterNo);

        return new BaseResult<IEnumerable<GetAllInvoicesResponse>>(responseData);
    }
}