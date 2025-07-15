using VoltMeter.Domain.Abstractions;

namespace VoltMeter.Domain.PtfRate;

public sealed class PtfRate : Entity
{
    public DateTime Period { get; set; }
    public decimal Price { get; set; }
}