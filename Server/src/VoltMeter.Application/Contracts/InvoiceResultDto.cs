namespace VoltMeter.Application.Contracts;

public class InvoiceResultDto
{
    public Guid MeterId { get; set; }
    public string MeterNo { get; set; } = string.Empty;
    public int Tariff { get; set; }
    public decimal Vat { get; set; }
    public decimal? Discount { get; set; }
    public decimal? Commission { get; set; }
    public int SalesMethod { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public decimal TotalConsumption { get; set; }
    public decimal TotalPtfAmount { get; set; }
    public decimal TotalYekAmount { get; set; }
    public decimal TotalEnergyTariffAmount { get; set; }
    public decimal TotalDistributionTariffAmount { get; set; }
    public decimal TotalAmount { get; set; }
}