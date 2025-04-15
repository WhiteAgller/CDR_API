using Application.CallRecords;
using Application.CallRecords.Queries;
using CDR_API.Mappers;
using FastEndpoints;
using MediatR;

namespace CDR_API.Endpoints.CallRecords;

public record GetByIdRequest
{
    public long Id { get; set; }
}

public record GetByIdResponse
{
    public CallRecordDto CallRecords { get; set; }
}

public class GetById(IMediator _mediator) : Endpoint<GetByIdRequest, GetByIdResponse>
{
    public override void Configure()
    {
        Get("callrecord/{Id:long}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetByIdRequest request, CancellationToken cancellationToken)
    {
        var callRecord = await _mediator.Send(new GetByIdQuery()
        {
            Id = request.Id
        }, cancellationToken);

        if (callRecord == null)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        Response = new GetByIdResponse()
        {
            CallRecords = CallRecordMapper.MapSingle(callRecord)
        };
    }
}
