using VoltMeter.Domain.Abstractions;
using VoltMeter.Domain.ConsumptionRecord;
using VoltMeter.Infrastructure.Context;

namespace VoltMeter.Infrastructure.Repositories;

public class ConsumptionRecordRepository : Repository<ConsumptionRecord, ApplicationDbContext>, IConsumptionRecordRepository
{
    public ConsumptionRecordRepository(ApplicationDbContext context) : base(context)
    {
    }
}