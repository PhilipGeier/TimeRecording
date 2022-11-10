using Microsoft.EntityFrameworkCore;
using TimeTracking.Data;
using TimeTracking.Domain;
using TimeTracking.Service.Interfaces;

namespace TimeTracking.Service;

public class TimeRecordingService : ITimeRecordingService
{
    private readonly DataContext _context;
    
    public TimeRecordingService(DataContext context)
    {
        _context = context;
    }
    
    public async Task<TimeRecording?> GetById(Guid id)
    {
        return await _context.TimeRecordings.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<TimeRecording>> GetAll()
    {
        return await _context.TimeRecordings.ToListAsync();
    }

    public async Task<IEnumerable<TimeRecording>> GetByUser(Guid userId)
    {
        return await _context.TimeRecordings.Where(x => x.User.Id == userId).ToListAsync();
    }

    public Task<TimeRecording>? CreateTimeRecording(TimeRecording timeRecording)
    {
        throw new NotImplementedException();
    }

    public Task<TimeRecording?> UpdateTimeRecording(Guid id, TimeRecording timeRecording)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TimeRecording>>? DeleteTimeRecording(Guid id)
    {
        throw new NotImplementedException();
    }
}