using System.Linq.Expressions;
using Application.CallRecords.BaseClasses;
using Domain.CallRecordAggregate;
using Domain.Interfaces;
using MediatR;

namespace Application.CallRecords.Queries;

public record GetTopOrBottomQuery : BaseAnalyticsQuery, IRequest<List<CallRecord>>
{
    public Expression<Func<CallRecord, object>> Column { get; set; }
}

public class GetTopOrBottomHandler : IRequestHandler<GetTopOrBottomQuery, List<CallRecord>>
{
    private readonly ICallRecordRepository _repository;

    public GetTopOrBottomHandler(ICallRecordRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CallRecord>> Handle(GetTopOrBottomQuery request, CancellationToken cancellationToken)
    {
        var input = new GetAnalyticsParams()
        {
            From = request.DateFrom,
            Limit = request.Limit,
            To = request.DateTo,
            Sort = request.Sort
        };
        return await _repository.GetTopOrBottom(request.Column, input, cancellationToken);
    }
}