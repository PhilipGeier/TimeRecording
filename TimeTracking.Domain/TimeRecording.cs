namespace TimeTracking.Domain;

public class TimeRecording
{
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }

    public TimeSpan? Span
    {
        get {
            if (EndTime is not null)
                return EndTime.Value.Subtract(StartTime);
            return null;
        }
    }

    public User User { get; set; }

}