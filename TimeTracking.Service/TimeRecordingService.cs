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

    public async Task<TimeRecording?> CreateTimeRecording(TimeRecording timeRecording)
    {
        try
        {
            await _context.TimeRecordings.AddAsync(timeRecording);
            await _context.SaveChangesAsync();
            return timeRecording;
        }
        catch
        {
            return null;
        }
    }

    public async Task<TimeRecording?> UpdateTimeRecording(Guid id, TimeRecording request)
    {
        var recording = await _context.TimeRecordings.FirstOrDefaultAsync(x=> x.Id == id);
        
        if (recording is null) return null;

        recording.StartTime = request.StartTime;
        recording.EndTime = request.EndTime;
        recording.User = request.User;

        await _context.SaveChangesAsync();
        
        return recording;
    }

    public async Task<IEnumerable<TimeRecording>?> DeleteTimeRecording(Guid id)
    {
        var recording = await _context.TimeRecordings.FirstOrDefaultAsync(x => x.Id == id);
        
        if (recording is null) return null;

        _context.TimeRecordings.Remove(recording);
        await _context.SaveChangesAsync();

        return await _context.TimeRecordings.ToListAsync();
    }
}