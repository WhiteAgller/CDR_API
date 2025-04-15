namespace Application.CallRecords.BaseClasses;

public record BaseAnalyticsQuery : BaseDateQuery
{
    public string? Sort { get; set; }
    public int? Limit { get; set; }
}
