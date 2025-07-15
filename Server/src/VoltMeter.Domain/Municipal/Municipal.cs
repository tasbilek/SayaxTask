using VoltMeter.Domain.Abstractions;

namespace VoltMeter.Domain.Municipal;

public sealed class Municipal : Entity
{
    public string Name { get; set; } = default!;
    public decimal LocalTax { get; set; }
    public ICollection<Meter.Meter> Meters { get; set; } = null!;
}