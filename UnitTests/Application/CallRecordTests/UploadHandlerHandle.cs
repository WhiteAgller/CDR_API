using Application.CallRecords.Commands;
using Domain.CallRecordAggregate;
using Domain.Interfaces;
using NSubstitute;
using Shouldly;
using Xunit;

namespace UnitTests.Application.CallRecordTests;

public class UploadHandlerHandle
{
    private readonly ICallRecordRepository _repository = Substitute.For<ICallRecordRepository>();
    private UploadCallRecordsCommandHandler _handler;

    public UploadHandlerHandle()
    {
        _handler = new UploadCallRecordsCommandHandler(_repository);
    }

    [Fact]
    public async Task Should_Throw_Empty_Stream()
    {
        await _repository.Upload(Arg.Any<List<CallRecord>>(), Arg.Any<CancellationToken>());
        Should.Throw<Exception>(async () =>
        {
            await _handler.Handle(new UploadCallRecordsCommand()
            {
                StreamReader = new StreamReader(Stream.Null)
            }, CancellationToken.None);
        });
    }
}