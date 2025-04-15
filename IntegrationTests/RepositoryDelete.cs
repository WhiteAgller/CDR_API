using IntegrationTests.Data;
using Shouldly;
using Xunit;

namespace IntegrationTests;

public class RepositoryDelete : Setup
{

   [Fact]
   public async Task Should_Delete_All()
   {
      var repository = GetRepository();
      await repository.Upload(CallRecordTestData.TestCallRecords, CancellationToken.None);

      var dateFrom = new DateTime(2000, 1, 1);
      var dateTo = new DateTime(2030, 1, 1);
      await repository.Delete(dateFrom, dateTo, CancellationToken.None);
      var callRecords = await repository.GetAll(dateFrom, dateTo, 0, 10, CancellationToken.None);
      
      callRecords.Count.ShouldBe(0);
   }
}