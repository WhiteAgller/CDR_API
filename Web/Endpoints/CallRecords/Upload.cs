using Application.CallRecords.Commands;
using FastEndpoints;
using MediatR;

namespace CDR_API.Endpoints.CallRecords;


public class Upload(IMediator _mediator) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post("callrecord/file");
        AllowFileUploads(dontAutoBindFormData: true);
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Uploads new file";
            s.Description = "Uploads .csv file with call records";
        });
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var failedSections = new List<string>();
        await foreach (var section in FormFileSectionsAsync(cancellationToken))
        {
            if (section is null)
            {
                failedSections.Add($"File was empty");
                continue;
            };
            try
            {
                using (var reader = new StreamReader(section.Section.Body))
                {
                    await _mediator.Send(new UploadCallRecordsCommand(){StreamReader = reader}, cancellationToken);
                }
            }
            catch (Exception e)
            {
                failedSections.Add($"There was an error in {section.FileName}");
            }
        }

        if (failedSections.Any())
        {
            AddError(string.Join(",", failedSections));
            await SendErrorsAsync(cancellation: cancellationToken);
            return;
        }
        await SendOkAsync("Upload completed", cancellationToken);
    }
}