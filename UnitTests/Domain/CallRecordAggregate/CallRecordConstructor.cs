using Domain.CallRecordAggregate;
using Shouldly;
using Xunit;

namespace UnitTests.Domain.CallRecordAggregate;

public class CallRecordConstructor
{
    private readonly string _callerId = "test callerId";
    private readonly string _recipient = "test recipient";
    private readonly DateTime _callDate = new(2020, 1, 1);
    private readonly TimeSpan _endTime = TimeSpan.FromMinutes(10);
    private readonly int _duration = 100;
    private readonly decimal _cost = 0.002m;
    private readonly string _reference = "test reference";
    private readonly Currency _currency  = Currency.GBP;

    private CallRecord CreateCallRecord()
    {
        return new CallRecord(_callerId, _recipient, _callDate, _endTime, _duration, _cost, _reference, _currency);
    }

    [Fact]
    public void ShouldCreateCallRecord()
    {
        var callRecord = CreateCallRecord();
        
        callRecord.CallerId.ShouldBe(_callerId);
        callRecord.Recipient.ShouldBe(_recipient);
        callRecord.CallDate.ShouldBe(_callDate);
        callRecord.EndTime.ShouldBe(_endTime);
        callRecord.Duration.ShouldBe(_duration);
        callRecord.Cost.ShouldBe(_cost);
        callRecord.Reference.ShouldBe(_reference);
        callRecord.Currency.ShouldBe(_currency);
    }
}
