using VoltMeter.Domain.Abstractions;

namespace VoltMeter.Domain.MonthlyRate;

public sealed class MonthlyRate : Entity
{
    public decimal YekPrice { get; set; }
    public decimal IndustrialEnergyTariff { get; set; }
    public decimal CommercialEnergyTariff { get; set; }
    public decimal IndustrialDistributionTariff { get; set; }
    public decimal CommercialDistributionTariff { get; set; }
}