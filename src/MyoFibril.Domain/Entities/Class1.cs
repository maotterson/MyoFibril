namespace MyoFibril.Domain.Entities;

public class Activity
{
    public Guid Guid { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTimeOffset DateCreated { get; set; }
    public long? StravaActivityId { get; set; }
}
