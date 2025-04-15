
using Application.CallRecords;
using Domain.CallRecordAggregate;

namespace CDR_API.Mappers;

public static class CallRecordMapper 
{
    public static List<CallRecordDto> MapToDto(List<CallRecord> callRecords)
    {
        return callRecords.Select(MapSingle).ToList();
    }

    public static CallRecordDto MapSingle(CallRecord callRecord)
    {
        return new CallRecordDto(
            callRecord.Id,
            callRecord.CallerId,
            callRecord.Recipient,
            callRecord.CallDate,
            callRecord.EndTime,
            callRecord.Duration,
            callRecord.Cost,
            callRecord.Reference,
            callRecord.Currency
        );
    }
}