using VoltMeter.Domain.Abstractions;
using VoltMeter.Domain.Meter;
using VoltMeter.Infrastructure.Context;

namespace VoltMeter.Infrastructure.Repositories;

public class MeterRepository : Repository<Meter, ApplicationDbContext>, IMeterRepository
{
    public MeterRepository(ApplicationDbContext context) : base(context)
    {
    }
}
