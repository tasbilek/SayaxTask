using VoltMeter.Domain.Abstractions;
using VoltMeter.Domain.Municipal;
using VoltMeter.Infrastructure.Context;

namespace VoltMeter.Infrastructure.Repositories;

public class MunicipalRepository : Repository<Municipal, ApplicationDbContext>, IMunicipalRepository
{
    public MunicipalRepository(ApplicationDbContext context) : base(context)
    {
    }
}