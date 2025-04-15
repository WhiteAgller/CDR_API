using Application.CallRecords.Commands;
using CDR_API.Endpoints.CallRecords.BaseClasses;
using FastEndpoints;
using MediatR;

namespace CDR_API.Endpoints.CallRecords;


public record DeleteRequest : BaseDateRequest
{
}

public class DeleteReponse
{
    public int DeletedItems { get; set; }
}

public class Delete(IMediator _mediator) : Endpoint<DeleteRequest, DeleteReponse>
{
    public override void Configure()
    {
        Delete("callrecord");
        AllowAnonymous();
        
    }

    public override async Task HandleAsync(DeleteRequest request, CancellationToken cancellationToken)
    {
        var deleted = await _mediator.Send(new DeleteCallRecordsCommand()
        {
            From = request.DateFrom,
            To = request.DateTo
        }, cancellationToken);

        Response.DeletedItems = deleted;
    }
}