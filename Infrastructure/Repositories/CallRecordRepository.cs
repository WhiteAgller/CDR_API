using System.Linq.Expressions;
using Application.CallRecords.Queries;
using Domain.CallRecordAggregate;
using Domain.Interfaces;
using EFCore.BulkExtensions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CallRecordRepository : ICallRecordRepository
{
    protected readonly AppDbContext _dbContext;

    public CallRecordRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<CallRecord>> GetAll(DateTime dateFrom, DateTime dateTo, int? skip, int? take, CancellationToken cancellationToken)
    {
        return await _dbContext.CallRecords
            .AsNoTracking()
            .IsBetweenDates(dateFrom, dateTo)
            .Skip(skip ?? 0)
            .Take(take ?? 10)
            .ToListAsync(cancellationToken);
    }

    public async Task<CallRecord?> GetById(long id, CancellationToken cancellationToken)
    {
        return await _dbContext.CallRecords
            .AsNoTracking()
            .FirstOrDefaultAsync(x => id == x.Id, cancellationToken);
    }

    public async Task<List<CallRecord>> GetTopOrBottom(Expression<Func<CallRecord, object>> column,
        GetAnalyticsParams input, CancellationToken cancellationToken)
    {
        var q = _dbContext.CallRecords
            .AsNoTracking()
            .IsBetweenDates(input.From, input.To);
        q = HandleSort(q, column, input.Sort);
        return await q
            .Take(input.Limit ?? 10)
            .ToListAsync(cancellationToken);
    }

    public async Task Upload(List<CallRecord> callRecords, CancellationToken cancellationToken)
    {
        await _dbContext.BulkInsertAsync(callRecords, cancellationToken: cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> Delete(DateTime from, DateTime to, CancellationToken cancellationToken)
    {
        return await _dbContext.CallRecords
            .IsBetweenDates(from, to)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<decimal> CalculateSum(Expression<Func<CallRecord, decimal>> column, DateTime from, DateTime to, CancellationToken cancellationToken)
    {
        return await _dbContext.CallRecords
            .AsNoTracking()
            .IsBetweenDates(from, to)
            .SumAsync(column, cancellationToken);
    }

    public async Task<decimal> CalculateAverage(Expression<Func<CallRecord, decimal>> column, DateTime from, DateTime to, CancellationToken cancellationToken)
    {
        return await _dbContext.CallRecords
            .AsNoTracking()
            .IsBetweenDates(from, to)
            .AverageAsync(column, cancellationToken);
    }

    private IQueryable<CallRecord> HandleSort(IQueryable<CallRecord> query, Expression<Func<CallRecord, object>> column,
        string? sort)
    {
        return !string.IsNullOrEmpty(sort) && sort.Equals("desc", StringComparison.CurrentCultureIgnoreCase)
            ? query.OrderByDescending(column)
            : query.OrderBy(column);
    }
}