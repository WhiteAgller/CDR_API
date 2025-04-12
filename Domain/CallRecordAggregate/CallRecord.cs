namespace Domain.CallRecordAggregate;

public class CallRecord(
     string callerId,
     string recipient,
     DateTime callDate,
     DateTime endTime,
     int duration,
     decimal cost,
     string reference,
     Currency currency)
{
     public long Id { get; set; }
     public string CallerId { get; set; } = callerId;
     public string Recipient { get; set; } = recipient;
     public DateTime CallDate { get; set; } = callDate;
     public DateTime EndTime { get; set; } = endTime;
     public int Duration { get; set; } = duration;
     public decimal Cost { get; set; } = cost;
     public string Reference { get; set; } = reference;
     public Currency Currency { get; set; } = currency;
}