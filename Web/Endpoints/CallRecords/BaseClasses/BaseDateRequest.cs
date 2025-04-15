using FastEndpoints;

namespace CDR_API.Endpoints.CallRecords.BaseClasses;

public record BaseDateRequest
{
    [QueryParam]
    public DateTime DateFrom { get; set; }
    
    [QueryParam]
    public DateTime DateTo { get; set; }
}