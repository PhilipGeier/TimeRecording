using TimeTracking.Domain;

namespace TimeTracking.Service.Interfaces;

public interface ITimeRecordingService
{
    Task<TimeRecording?> GetById(Guid id);
    Task<IEnumerable<TimeRecording>> GetAll();
    Task<IEnumerable<TimeRecording>?> GetByUser(Guid userId);
    Task<TimeRecording?> CreateTimeRecording(TimeRecording timeRecording);
    Task<TimeRecording?> StartRecording(DateTime startTime);
    Task<TimeRecording?> EndRecording(Guid id, DateTime endTime);
    Task<TimeRecording?> UpdateTimeRecording(Guid id, TimeRecording request);
    Task<IEnumerable<TimeRecording>?> DeleteTimeRecording(Guid id);
}