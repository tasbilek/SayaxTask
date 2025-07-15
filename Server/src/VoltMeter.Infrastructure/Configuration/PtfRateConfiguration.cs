using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VoltMeter.Domain.PtfRate;

namespace VoltMeter.Infrastructure.Configuration;

public class PtfRateConfiguration : IEntityTypeConfiguration<PtfRate>
{
    public void Configure(EntityTypeBuilder<PtfRate> builder)
    {
        builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
    }
}