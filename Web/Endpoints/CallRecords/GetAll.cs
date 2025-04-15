using Application.CallRecords;
using Application.CallRecords.Queries;
using CDR_API.Endpoints.CallRecords.BaseClasses;
using CDR_API.Mappers;
using FastEndpoints;
using MediatR;

namespace CDR_API.Endpoints.CallRecords;

public record GetAllRequest : BaseDateRequest
{
    public int? Skip { get; set; }

    public int? Take { get; set; }
}

public record GetAllResponse
{
    public List<CallRecordDto> CallRecords { get; set; }
}

public class GetAll(IMediator _mediator) : Endpoint<GetAllRequest, GetAllResponse>
{
    public override void Configure()
    {
        Get("callrecord");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetAllRequest request, CancellationToken cancellationToken)
    {
        var callRecords = await _mediator.Send(new GetAllQuery()
        {
            Skip = request.Skip,
            Take = request.Take,
            DateFrom = request.DateFrom,
            DateTo = request.DateTo
        }, cancellationToken);

        Response = new GetAllResponse()
        {
            CallRecords = CallRecordMapper.MapToDto(callRecords)
        };
    }
}