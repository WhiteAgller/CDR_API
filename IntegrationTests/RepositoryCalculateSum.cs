using System.Linq.Expressions;
using Domain.CallRecordAggregate;
using IntegrationTests.Data;
using Shouldly;
using Xunit;

namespace IntegrationTests;

public class RepositoryCalculateSum : Setup
{
    [Fact]
    public async Task Should_Calculate_Sum_Cost()
    {
        var repository = GetRepository();
        await repository.Upload(CallRecordTestData.TestCallRecords, CancellationToken.None);
        
        Expression<Func<CallRecord, decimal>> column = record => record.Cost;
        var sum = await repository.CalculateSum(column, new (2010, 1, 1), new (2026, 1, 1), CancellationToken.None);
        
        sum.ShouldBe(8.25m);
    }
    
    [Fact]
    public async Task Should_Calculate_Sum_Duration()
    {
        var repository = GetRepository();
        await repository.Upload(CallRecordTestData.TestCallRecords, CancellationToken.None);
        
        Expression<Func<CallRecord, decimal>> column = record => record.Duration;
        var sum = await repository.CalculateSum(column, new (2010, 1, 1), new (2026, 1, 1), CancellationToken.None);
        
        sum.ShouldBe(425);
    }
}