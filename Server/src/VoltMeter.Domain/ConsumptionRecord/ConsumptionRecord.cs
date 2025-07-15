using VoltMeter.Domain.Abstractions;

namespace VoltMeter.Domain.ConsumptionRecord;

public sealed class ConsumptionRecord : Entity
{
    public DateTime ReadingDay { get; set; }
    public int ReadingHour { get; set; }
    public decimal ReadingValue { get; set; }

    public Guid MeterId { get; set; }
    public Meter.Meter Meter { get; set; } = null!;
}