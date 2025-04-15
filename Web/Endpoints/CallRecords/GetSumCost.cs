using System.Linq.Expressions;
using Application.CallRecords.Queries;
using CDR_API.Endpoints.CallRecords.BaseClasses;
using Domain.CallRecordAggregate;
using FastEndpoints;
using MediatR;

namespace CDR_API.Endpoints.CallRecords;

public record GetSumCostRequest : BaseDateRequest
{
}

public record GetSumCostResponse
{
    public decimal Sum { get; set; }
}

public class GetSumCost(IMediator _mediator) : Endpoint<GetSumCostRequest, GetSumCostResponse>
{
    public override void Configure()
    {
        Get("callrecord/sum_costs");
        AllowAnonymous();
        Summary(s => { s.Description = "Gets sum of costs"; });
    }

    public override async Task HandleAsync(GetSumCostRequest req, CancellationToken ct)
    {
        Expression<Func<CallRecord, decimal>> column = record => record.Cost;
        var sum = await _mediator.Send(new GetSumQuery()
        {
            DateFrom = req.DateFrom,
            DateTo = req.DateTo,
            column = column
        }, ct);

        Response = new GetSumCostResponse()
        {
            Sum = sum
        };
    }
}