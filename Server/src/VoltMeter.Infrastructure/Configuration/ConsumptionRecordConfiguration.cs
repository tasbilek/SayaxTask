using Microsoft.EntityFrameworkCore;
using VoltMeter.Domain.ConsumptionRecord;

namespace VoltMeter.Infrastructure.Configuration;

public class ConsumptionRecordConfiguration : IEntityTypeConfiguration<ConsumptionRecord>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ConsumptionRecord> builder)
    {
        builder.Property(p => p.ReadingValue).HasColumnType("decimal(18,4)");
        builder.HasOne(h => h.Meter)
               .WithMany(m => m.ConsumptionRecords)
               .HasForeignKey(h => h.MeterId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
