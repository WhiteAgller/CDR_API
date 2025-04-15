using System.Linq.Expressions;
using Application.CallRecords.Queries;
using CDR_API.Endpoints.CallRecords.BaseClasses;
using CDR_API.Mappers;
using Domain.CallRecordAggregate;
using FastEndpoints;
using MediatR;

namespace CDR_API.Endpoints.CallRecords;

public record GetTopOrBottomCostsRequests : BaseAnalyticsRequest { }

public class GetTopOrBottomCost(IMediator _mediator) : Endpoint<GetTopOrBottomCostsRequests, GetAllResponse>
{
    public override void Configure()
    {
        Get("callrecord/top_or_bottom_cost");
        AllowAnonymous();
        Summary(s =>
        {
            s.Description = "Gets top n or bottom n records sorted by cost";
            s.Params[nameof(BaseAnalyticsRequest.Sort)] = "asc or desc, default is asc"; 
            s.Params[nameof(BaseAnalyticsRequest.Limit)] = "default is 10"; 
        });
    }

    public override async Task HandleAsync(GetTopOrBottomCostsRequests req, CancellationToken ct)
    {
        Expression<Func<CallRecord, object>> column = record => record.Cost; 
        var callRecords = await _mediator.Send(new GetTopOrBottomQuery()
        {
            DateFrom = req.DateFrom,
            DateTo = req.DateTo,
            Limit = req.Limit,
            Sort = req.Sort,
            Column = column
        }, ct);
        
        Response = new GetAllResponse()
        {
            CallRecords = CallRecordMapper.MapToDto(callRecords)
        };
    }
}