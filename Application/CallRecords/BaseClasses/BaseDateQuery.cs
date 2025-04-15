namespace Application.CallRecords.BaseClasses;

public record BaseDateQuery
{
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
}