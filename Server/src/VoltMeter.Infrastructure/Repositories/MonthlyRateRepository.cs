using VoltMeter.Domain.Abstractions;
using VoltMeter.Domain.MonthlyRate;
using VoltMeter.Infrastructure.Context;

namespace VoltMeter.Infrastructure.Repositories;

public class MonthlyRateRepository : Repository<MonthlyRate, ApplicationDbContext>, IMonthlyRateRepository
{
    public MonthlyRateRepository(ApplicationDbContext context) : base(context)
    {
    }
}