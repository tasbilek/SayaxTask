using VoltMeter.Domain.Abstractions;

namespace VoltMeter.Domain.Meter;

public sealed class Meter : Entity
{
    public string No { get; set; } = default!;
    public string? Description { get; set; }
    public decimal? Commission { get; set; }
    public decimal? Discount { get; set; }
    public decimal Vat { get; set; }
    public int Tariff { get; set; }
    public int SalesMethod { get; set; }

    public Guid MunicipalId { get; set; }
    public Municipal.Municipal Municipal { get; set; } = null!;
    public ICollection<ConsumptionRecord.ConsumptionRecord> ConsumptionRecords { get; set; } = null!;
}
