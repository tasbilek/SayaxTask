using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VoltMeter.Domain.MonthlyRate;

namespace VoltMeter.Infrastructure.Configuration;

public class MonthlyRateConfiguration : IEntityTypeConfiguration<MonthlyRate>
{
    public void Configure(EntityTypeBuilder<MonthlyRate> builder)
    {
        builder.Property(p => p.YekPrice).HasColumnType("decimal(18,2)");
        builder.Property(p => p.IndustrialEnergyTariff).HasColumnType("decimal(18,2)");
        builder.Property(p => p.CommercialEnergyTariff).HasColumnType("decimal(18,2)");
        builder.Property(p => p.IndustrialDistributionTariff).HasColumnType("decimal(18,2)");
        builder.Property(p => p.CommercialDistributionTariff).HasColumnType("decimal(18,2)");
    }
}