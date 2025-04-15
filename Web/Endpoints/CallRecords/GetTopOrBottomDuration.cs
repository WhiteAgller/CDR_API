using System.Linq.Expressions;
using Application.CallRecords.Queries;
using CDR_API.Endpoints.CallRecords.BaseClasses;
using CDR_API.Mappers;
using Domain.CallRecordAggregate;
using FastEndpoints;
using MediatR;

namespace CDR_API.Endpoints.CallRecords;

public record GetTopOrBottomDurationRequests : BaseAnalyticsRequest { }

public class GetTopOrBottomDuration(IMediator _mediator) : Endpoint<GetTopOrBottomDurationRequests, GetAllResponse>
{
    public override void Configure()
    {
        Get("callrecord/top_or_bottom_duration");
        AllowAnonymous();
        Summary(s =>
        {
            s.Description = "Gets top n or bottom n records sorted by duration";
            s.Params[nameof(BaseAnalyticsRequest.Sort)] = "asc or desc, default is asc"; 
            s.Params[nameof(BaseAnalyticsRequest.Limit)] = "default is 10"; 
        });
    }

    public override async Task HandleAsync(GetTopOrBottomDurationRequests req, CancellationToken ct)
    {
        Expression<Func<CallRecord, object>> column = record => record.Duration; 
        var callRecords = await _mediator.Send(new GetTopOrBottomQuery()
        {
            DateFrom = req.DateFrom,
            DateTo = req.DateTo,
            Limit = req.Limit,
            Sort = req.Sort,
            Column = column,
        }, ct);
        
        Response = new GetAllResponse()
        {
            CallRecords = CallRecordMapper.MapToDto(callRecords)
        };
    }
}