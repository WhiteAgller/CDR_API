namespace Application.CallRecords.Queries;

public class GetAnalyticsParams
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public string? Sort { get; set; }
    public int? Limit { get; set; }
}