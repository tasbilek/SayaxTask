using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VoltMeter.Domain.Municipal;

namespace VoltMeter.Infrastructure.Configuration;

public class MunicipalConfiguration : IEntityTypeConfiguration<Municipal>
{
    public void Configure(EntityTypeBuilder<Municipal> builder)
    {
        builder.Property(x => x.LocalTax).HasColumnType("decimal(8,4)");
    }
}
