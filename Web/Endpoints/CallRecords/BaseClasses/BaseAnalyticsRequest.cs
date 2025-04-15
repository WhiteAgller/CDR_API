using System.ComponentModel;

namespace CDR_API.Endpoints.CallRecords.BaseClasses;

public record BaseAnalyticsRequest : BaseDateRequest
{
    [DefaultValue("asc")]
    public string? Sort { get; set; }
    
    [DefaultValue(10)]
    public int? Limit { get; set; }
}