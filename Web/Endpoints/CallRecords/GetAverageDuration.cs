using System.Linq.Expressions;
using Application.CallRecords.Queries;
using CDR_API.Endpoints.CallRecords.BaseClasses;
using Domain.CallRecordAggregate;
using FastEndpoints;
using MediatR;

namespace CDR_API.Endpoints.CallRecords;
public record GetAverageDurationRequest : BaseDateRequest
{
}

public record GetAverageDurationResponse
{
    public decimal Average { get; set; }
}

public class GetAverageDuration(IMediator _mediator) : Endpoint<GetAverageDurationRequest, GetAverageDurationResponse>
{
    public override void Configure()
    {
        Get("callrecord/average_durations");
        AllowAnonymous();
        Summary(s => { s.Description = "Gets average of durations"; });
    }

    public override async Task HandleAsync(GetAverageDurationRequest req, CancellationToken ct)
    {
        Expression<Func<CallRecord, decimal>> column = record => record.Duration;
        var average = await _mediator.Send(new GetAverageQuery()
        {
            DateFrom = req.DateFrom,
            DateTo = req.DateTo,
            column = column
        }, ct);

        Response = new GetAverageDurationResponse()
        {
            Average = average
        };
    }
}
