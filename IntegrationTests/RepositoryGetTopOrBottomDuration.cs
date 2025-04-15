using System.Linq.Expressions;
using Application.CallRecords.Queries;
using Domain.CallRecordAggregate;
using IntegrationTests.Data;
using Shouldly;
using Xunit;

namespace IntegrationTests;

public class RepositoryGetTopOrBottomDuration : Setup
{
    [Fact]
    public async Task Should_Get_Top_2_Duration()
    {
        var repository = GetRepository();

        await repository.Upload(CallRecordTestData.TestCallRecords, CancellationToken.None);
        var input = new GetAnalyticsParams()
        {
            From = new(2024, 1,1),
            To = new(2026, 1,1),
            Limit = 10,
            Sort = "asc"
        };
        Expression<Func<CallRecord, object>> column = record => record.Duration; 
        var analytics = await repository.GetTopOrBottom(column, input, CancellationToken.None);
        
        analytics.Count.ShouldBe(2);
        analytics[0].Duration.ShouldBe(125);
        analytics[1].Duration.ShouldBe(300);
    }
    
    [Fact]
    public async Task Should_Get_Bottom_2_Duration()
    {
        var repository = GetRepository();

        await repository.Upload(CallRecordTestData.TestCallRecords, CancellationToken.None);
        var input = new GetAnalyticsParams()
        {
            From = new(2024, 1,1),
            To = new(2026, 1,1),
            Limit = 10,
            Sort = "desc"
        };
        Expression<Func<CallRecord, object>> column = record => record.Duration; 
        var analytics = await repository.GetTopOrBottom(column, input, CancellationToken.None);
        
        analytics.Count.ShouldBe(2);
        analytics[0].Duration.ShouldBe(300);
        analytics[1].Duration.ShouldBe(125);
    }
    
    [Fact]
    public async Task Should_Get_Max_Duration()
    {
        var repository = GetRepository();

        await repository.Upload(CallRecordTestData.TestCallRecords, CancellationToken.None);
        var input = new GetAnalyticsParams()
        {
            From = new(2024, 1,1),
            To = new(2026, 1,1),
            Limit = 1,
            Sort = "desc"
        };
        Expression<Func<CallRecord, object>> column = record => record.Duration; 
        var analytics = await repository.GetTopOrBottom(column, input, CancellationToken.None);
        
        analytics.Count.ShouldBe(1);
        analytics[0].Duration.ShouldBe(300);
    }
    
    [Fact]
    public async Task Should_Get_Min_Duration()
    {
        var repository = GetRepository();

        await repository.Upload(CallRecordTestData.TestCallRecords, CancellationToken.None);
        var input = new GetAnalyticsParams()
        {
            From = new(2024, 1,1),
            To = new(2026, 1,1),
            Limit = 1,
            Sort = "asc"
        };
        Expression<Func<CallRecord, object>> column = record => record.Duration; 
        var analytics = await repository.GetTopOrBottom(column, input, CancellationToken.None);
        
        analytics.Count.ShouldBe(1);
        analytics[0].Duration.ShouldBe(125);
    }
}