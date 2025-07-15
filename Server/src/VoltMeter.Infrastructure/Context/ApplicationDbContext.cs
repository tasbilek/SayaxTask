using Microsoft.EntityFrameworkCore;
using VoltMeter.Domain.Abstractions;
using VoltMeter.Domain.ConsumptionRecord;
using VoltMeter.Domain.Meter;
using VoltMeter.Domain.MonthlyRate;
using VoltMeter.Domain.Municipal;
using VoltMeter.Domain.PtfRate;

namespace VoltMeter.Infrastructure.Context;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ConsumptionRecord> ConsumptionRecords { get; set; }
    public DbSet<Meter> Meters { get; set; }
    public DbSet<MonthlyRate> MonthlyRates { get; set; }
    public DbSet<Municipal> Municipals { get; set; }
    public DbSet<PtfRate> PtfRates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
