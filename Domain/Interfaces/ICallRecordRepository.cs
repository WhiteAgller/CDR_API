using System.Linq.Expressions;
using Application.CallRecords.Queries;
using Domain.CallRecordAggregate;

namespace Domain.Interfaces;

public interface ICallRecordRepository
{
    Task<List<CallRecord>> GetAll(DateTime dateFrom, DateTime dateTo, int? skip, int? take, CancellationToken cancellationToken);

    Task<CallRecord?> GetById(long id, CancellationToken cancellationToken);

    Task<List<CallRecord>> GetTopOrBottom(Expression<Func<CallRecord, object>> column,
        GetAnalyticsParams input, CancellationToken cancellationToken);

    Task Upload(List<CallRecord> callRecords, CancellationToken cancellationToken);
    
    Task<int> Delete(DateTime from, DateTime to, CancellationToken cancellationToken); 
    
    Task<decimal> CalculateSum(Expression<Func<CallRecord, decimal>> column, DateTime from, DateTime to, CancellationToken cancellationToken);
    
    Task<decimal> CalculateAverage(Expression<Func<CallRecord, decimal>> column, DateTime from, DateTime to, CancellationToken cancellationToken);
}