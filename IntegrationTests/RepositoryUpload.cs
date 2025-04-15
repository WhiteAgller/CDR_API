using IntegrationTests.Data;
using Shouldly;
using Xunit;

namespace IntegrationTests;

public class RepositoryUpload : Setup
{
    [Fact]
    public async Task Should_Upload_File()
    {
        var repository = GetRepository();
        await repository.Upload(CallRecordTestData.TestCallRecords, CancellationToken.None);

        var callRecords = await repository.GetAll(
            new DateTime(2024,1,1),
            new DateTime(2026,1,1),
            0,
            10,
            CancellationToken.None);
        
        callRecords.Count.ShouldBe(2);
        var first = callRecords[0];
        first.ShouldNotBeNull();
        first.CallerId.ShouldBe(CallRecordTestData.CallerId1);
        first.Reference.ShouldBe(CallRecordTestData.Reference1);
        first.CallDate.ShouldBe(CallRecordTestData.CallDate1);
        first.EndTime.ShouldBe(CallRecordTestData.EndTime1);
        first.Duration.ShouldBe(CallRecordTestData.Duration1);
        first.Cost.ShouldBe(CallRecordTestData.Cost1);
        first.Reference.ShouldBe(CallRecordTestData.Reference1);
        first.Currency.ShouldBe(CallRecordTestData.Currency1);
        
        var second = callRecords[1];
        second.ShouldNotBeNull();
        second.CallerId.ShouldBe(CallRecordTestData.CallerId2);
        second.Reference.ShouldBe(CallRecordTestData.Reference2);
        second.CallDate.ShouldBe(CallRecordTestData.CallDate2);
        second.EndTime.ShouldBe(CallRecordTestData.EndTime2);
        second.Duration.ShouldBe(CallRecordTestData.Duration2);
        second.Cost.ShouldBe(CallRecordTestData.Cost2);
        second.Reference.ShouldBe(CallRecordTestData.Reference2);
        second.Currency.ShouldBe(CallRecordTestData.Currency2);
    }

    [Fact]
    public async Task Should_Upload_Empty_File_And_Delete()
    {
        var repository = GetRepository();
        await repository.Upload([], CancellationToken.None);
        
        var callRecords = await repository.GetAll(
            new DateTime(2024,1,1),
            new DateTime(2026,1,1),
            0,
            10,
            CancellationToken.None);
        
        callRecords.Count.ShouldBe(0);
    }
    
}