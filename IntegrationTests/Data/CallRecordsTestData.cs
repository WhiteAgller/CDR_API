using Domain.CallRecordAggregate;

namespace IntegrationTests.Data;

public static class CallRecordTestData
{
    public static string CallerId1 => "420123456789";
    public static string Recipient1 => "420987654321";
    public static DateTime CallDate1 = new (2025, 4, 14);
    public static TimeSpan EndTime1 => new (20, 15, 30);
    public static int Duration1 => 125;
    public static decimal Cost1 => 2.50m;
    public static string Reference1 => "CALL-001";
    public static Currency Currency1 => Currency.GBP;

    public static string CallerId2 => "1445551212";
    public static string Recipient2 => "13334445555";
    public static DateTime CallDate2 => new (2025, 4, 13);
    public static TimeSpan EndTime2 => new (10, 05, 00);
    public static int Duration2 => 300;
    public static decimal Cost2 => 5.75m;
    public static string Reference2 => "CALL-002";
    public static Currency Currency2 => Currency.GBP;
    
     public static List<CallRecord> TestCallRecords =>
     [
         new(
             callerId: CallerId1,
             recipient: Recipient1,
             callDate: CallDate1,
             endTime: EndTime1,
             duration: Duration1,
             cost: Cost1,
             reference: Reference1,
             currency: Currency1
         ),

         new(
             callerId: CallerId2,
             recipient: Recipient2,
             callDate: CallDate2,
             endTime: EndTime2,
             duration: Duration2,
             cost: Cost2,
             reference: Reference2,
             currency: Currency2
         )
     ];
}