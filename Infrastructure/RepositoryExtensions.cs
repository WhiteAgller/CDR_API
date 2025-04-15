using Domain.CallRecordAggregate;

namespace Infrastructure;

public static class RepositoryExtensions
{
    public static IQueryable<CallRecord> IsBetweenDates(this IQueryable<CallRecord> q, DateTime from, DateTime to)
    {
        return q.Where(x => from <= x.CallDate && x.CallDate <= to);
    }
}