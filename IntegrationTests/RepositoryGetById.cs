using IntegrationTests.Data;
using Shouldly;
using Xunit;

namespace IntegrationTests;

public class RepositoryGetById : Setup
{
    [Fact]
    public async Task Should_Get_By_Id()
    {
        var repository = GetRepository();
        await repository.Upload(CallRecordTestData.TestCallRecords, CancellationToken.None);
        var callRecord = await repository.GetById(1, CancellationToken.None);
        callRecord.ShouldNotBeNull();
        callRecord.Id.ShouldBe(1);
        callRecord.CallerId.ShouldBe(CallRecordTestData.CallerId1);
        callRecord.Reference.ShouldBe(CallRecordTestData.Reference1);
        callRecord.CallDate.ShouldBe(CallRecordTestData.CallDate1);
        callRecord.EndTime.ShouldBe(CallRecordTestData.EndTime1);
        callRecord.Duration.ShouldBe(CallRecordTestData.Duration1);
        callRecord.Cost.ShouldBe(CallRecordTestData.Cost1);
        callRecord.Reference.ShouldBe(CallRecordTestData.Reference1);
        callRecord.Currency.ShouldBe(CallRecordTestData.Currency1);
    }
    
    [Fact]
    public async Task Should_Not_Get_By_Id()
    {
        var repository = GetRepository();
        var callRecord = await repository.GetById(101, CancellationToken.None);
        callRecord.ShouldBeNull();
    }
    
}