using System.Linq.Expressions;
using Application.CallRecords.Queries;
using CDR_API.Endpoints.CallRecords.BaseClasses;
using Domain.CallRecordAggregate;
using FastEndpoints;
using MediatR;

namespace CDR_API.Endpoints.CallRecords;
public record GetSumDurationRequest : BaseDateRequest
{
}

public record GetSumDurationResponse
{
    public decimal Sum { get; set; }
}

public class GetSumDuration(IMediator _mediator) : Endpoint<GetSumDurationRequest, GetSumDurationResponse>
{
    public override void Configure()
    {
        Get("callrecord/sum_durations");
        AllowAnonymous();
        Summary(s => { s.Description = "Gets sum of durations"; });
    }

    public override async Task HandleAsync(GetSumDurationRequest req, CancellationToken ct)
    {
        Expression<Func<CallRecord, decimal>> column = record => record.Duration;
        var sum = await _mediator.Send(new GetSumQuery()
        {
            DateFrom = req.DateFrom,
            DateTo = req.DateTo,
            column = column
        }, ct);

        Response = new GetSumDurationResponse()
        {
            Sum = sum
        };
    }
}
