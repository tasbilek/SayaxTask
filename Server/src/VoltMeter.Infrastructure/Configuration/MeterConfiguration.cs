using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VoltMeter.Domain.Meter;

namespace VoltMeter.Infrastructure.Configuration;

public class MeterConfiguration : IEntityTypeConfiguration<Meter>
{
    public void Configure(EntityTypeBuilder<Meter> builder)
    {
        builder.Property(p => p.Commission).HasColumnType("decimal(8,4)");
        builder.Property(p => p.Discount).HasColumnType("decimal(8,4)");
        builder.Property(p => p.Vat).HasColumnType("decimal(8,4)");
        builder.HasOne(h => h.Municipal)
               .WithMany(m => m.Meters)
               .HasForeignKey(m => m.MunicipalId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}