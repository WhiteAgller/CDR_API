using System.Linq.Expressions;
using Application.CallRecords.Queries;
using CDR_API.Endpoints.CallRecords.BaseClasses;
using Domain.CallRecordAggregate;
using FastEndpoints;
using MediatR;

namespace CDR_API.Endpoints.CallRecords;
public record GetAverageCostRequest : BaseDateRequest
{
}

public record GetAverageCostResponse
{
    public decimal Average { get; set; }
}

public class GetAverageCost(IMediator _mediator) : Endpoint<GetAverageCostRequest, GetAverageCostResponse>
{
    public override void Configure()
    {
        Get("callrecord/average_costs");
        AllowAnonymous();
        Summary(s => { s.Description = "Gets average of costs"; });
    }

    public override async Task HandleAsync(GetAverageCostRequest req, CancellationToken ct)
    {
        Expression<Func<CallRecord, decimal>> column = record => record.Cost;
        var average = await _mediator.Send(new GetAverageQuery()
        {
            DateFrom = req.DateFrom,
            DateTo = req.DateTo,
            column = column
        }, ct);

        Response = new GetAverageCostResponse()
        {
            Average = average
        };
    }
}
