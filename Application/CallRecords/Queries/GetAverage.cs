using System.Linq.Expressions;
using Application.CallRecords.BaseClasses;
using Domain.CallRecordAggregate;
using Domain.Interfaces;
using MediatR;

namespace Application.CallRecords.Queries;
public record GetAverageQuery : BaseDateQuery, IRequest<decimal>
{
    public Expression<Func<CallRecord, decimal>> column { get; set; }
}

public class GetAverageQueryHandler : IRequestHandler<GetAverageQuery, decimal>
{
    private readonly ICallRecordRepository _repository;

    public GetAverageQueryHandler(ICallRecordRepository repository)
    {
        _repository = repository;
    }

    public async Task<decimal> Handle(GetAverageQuery request, CancellationToken cancellationToken)
    {
        return await _repository.CalculateAverage(request.column, request.DateFrom, request.DateTo, cancellationToken);
    }
}
