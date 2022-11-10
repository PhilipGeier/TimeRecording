using TimeTracking.Domain;

namespace TimeTracking.Service.Interfaces;

public interface ITimeRecordingService
{
    Task<TimeRecording?> GetById(Guid id);
    Task<IEnumerable<TimeRecording>> GetAll();
    Task<IEnumerable<TimeRecording>> GetByUser(Guid userId);
    Task<TimeRecording?> CreateTimeRecording(TimeRecording timeRecording);
    Task<TimeRecording?> UpdateTimeRecording(Guid id, TimeRecording timeRecording);
    Task<IEnumerable<TimeRecording>?> DeleteTimeRecording(Guid id);
}