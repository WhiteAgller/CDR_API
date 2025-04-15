using Domain.CallRecordAggregate;
using Domain.Extensions;
using Domain.Interfaces;
using MediatR;

namespace Application.CallRecords.Commands;

public record UploadCallRecordsCommand : IRequest<Task>
{
    public StreamReader StreamReader { get; init; }
}

public class UploadCallRecordsCommandHandler : IRequestHandler<UploadCallRecordsCommand, Task>
{
    private readonly ICallRecordRepository _repository;
    private static readonly string[] ExpectedHeaders = ["caller_id","recipient","call_date","end_time","duration","cost","reference","currency"];

    public UploadCallRecordsCommandHandler(ICallRecordRepository repository)
    {
        _repository = repository;
    }

    public async Task<Task> Handle(UploadCallRecordsCommand request, CancellationToken cancellationToken)
    {
        var callRecords = new List<CallRecord>();
        if (request.StreamReader.BaseStream == Stream.Null)
        {
            throw new Exception("Empty stream");
        }
        while (await request.StreamReader.ReadLineAsync(cancellationToken) is { } line)
        {
            var fields = line.Split(',');
            
            if (IsHeader(fields)) continue; 
            
            var callRecord = new CallRecord(
                fields[0],
                fields[1],
                fields[2].ParseToDateTime(),
                fields[3].ParseToTimeSpan(),
                fields[4].ParseToInt(),
                fields[5].ParseToDecimal(),
                fields[6],
                fields[7].ParseToCurrency<Currency>());
            callRecords.Add(callRecord);
        }
        await _repository.Upload(callRecords, cancellationToken);
        return Task.CompletedTask;
    }

    private bool IsHeader(string[] fields)
    {
        return ExpectedHeaders.All(fields.Contains);
    }
}
