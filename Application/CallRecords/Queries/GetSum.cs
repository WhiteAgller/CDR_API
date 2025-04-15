using System.Linq.Expressions;
using Application.CallRecords.BaseClasses;
using Domain.CallRecordAggregate;
using Domain.Interfaces;
using MediatR;

namespace Application.CallRecords.Queries;
public record GetSumQuery : BaseDateQuery, IRequest<decimal>
{
    public Expression<Func<CallRecord, decimal>> column { get; set; }
}

public class GetSumQueryHandler : IRequestHandler<GetSumQuery, decimal>
{
    private readonly ICallRecordRepository _repository;

    public GetSumQueryHandler(ICallRecordRepository repository)
    {
        _repository = repository;
    }

    public async Task<decimal> Handle(GetSumQuery request, CancellationToken cancellationToken)
    {
        return await _repository.CalculateSum(request.column, request.DateFrom, request.DateTo, cancellationToken);
    }
}
