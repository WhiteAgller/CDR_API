using System.Linq.Expressions;
using Domain.CallRecordAggregate;
using IntegrationTests.Data;
using Shouldly;
using Xunit;

namespace IntegrationTests;

public class RepositoryCalculateAverage : Setup
{
    
    [Fact]
    public async Task Should_Calculate_Average_Cost()
    {
        var repository = GetRepository();
        await repository.Upload(CallRecordTestData.TestCallRecords, CancellationToken.None);
        
        Expression<Func<CallRecord, decimal>> column = record => record.Cost;
        var average = await repository.CalculateAverage(column, new (2010, 1, 1), new (2026, 1, 1), CancellationToken.None);
        
        average.ShouldBe(4.125m);
    }
    
    [Fact]
    public async Task Should_Calculate_Average_Duration()
    {
        var repository = GetRepository();
        await repository.Upload(CallRecordTestData.TestCallRecords, CancellationToken.None);
        
        Expression<Func<CallRecord, decimal>> column = record => record.Duration;
        var average = await repository.CalculateAverage(column, new (2010, 1, 1), new (2026, 1, 1), CancellationToken.None);
        
        average.ShouldBe(212.5m);
    }
}