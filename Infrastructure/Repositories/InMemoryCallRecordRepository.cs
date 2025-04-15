using Domain.CallRecordAggregate;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class InMemoryCallRecordRepository : CallRecordRepository, ICallRecordRepository
{
    public InMemoryCallRecordRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public new async Task Upload(List<CallRecord> callRecords, CancellationToken cancellationToken)
    {
        await _dbContext.AddRangeAsync(callRecords, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public new async Task Delete(DateTime from, DateTime to, CancellationToken cancellationToken)
    {
        var records = await _dbContext.CallRecords
            .IsBetweenDates(from, to)
            .ToListAsync(cancellationToken);
        _dbContext.CallRecords.RemoveRange(records);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}