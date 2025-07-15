using VoltMeter.Domain.Abstractions;
using VoltMeter.Domain.PtfRate;
using VoltMeter.Infrastructure.Context;

namespace VoltMeter.Infrastructure.Repositories;

public class PtfRateRepository : Repository<PtfRate, ApplicationDbContext>, IPtfRateRepository
{
    public PtfRateRepository(ApplicationDbContext context) : base(context)
    {
    }
}