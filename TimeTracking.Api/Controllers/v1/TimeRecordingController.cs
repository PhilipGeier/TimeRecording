using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeTracking.Domain;
using TimeTracking.Service.Interfaces;

namespace TimeTracking.Api.Controllers.v1;

[Route("/api/[controller]")]
[ApiController]
public class TimeRecordingController : ControllerBase
{
    private readonly ITimeRecordingService _service;
    
    public TimeRecordingController(ITimeRecordingService service)
    {
        _service = service;
    }
    
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<TimeRecording>>> GetAll()
    {
        return Ok(await _service.GetAll());
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<TimeRecording>> GetById(Guid id)
    {
        var result = await _service.GetById(id);
        
        if (result is null) return NotFound("Time Recording does not exist");

        return Ok(result);
    }

    [HttpGet("user/{id:guid}")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<TimeRecording>>> GetByUser(Guid id)
    {
        var result = await _service.GetByUser(id);

        if (result is null) return NotFound("Time Recording does not exist");

        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<TimeRecording>> Create(TimeRecording recording)
    {
        var result = await _service.CreateTimeRecording(recording);

        if (result is null) return Forbid("Time Recording already exists");

        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<TimeRecording>> Update(Guid id, TimeRecording timeRecording)
    {
        var result = await _service.UpdateTimeRecording(id, timeRecording);

        if (result is null) return NotFound("Time Recording does not exist");

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<TimeRecording>>> Delete(Guid id)
    {
        var result = await _service.DeleteTimeRecording(id);

        if (result is null) return NotFound("Time Recording does not exist");

        return Ok(result);
    }

    [HttpPost("start/")]
    [Authorize]
    public async Task<ActionResult<TimeRecording>> StartRecording(DateTime startTime)
    {
        var result = await _service.StartRecording(startTime);

        if (result is null)
            return NotFound("Error while starting recording");

        return result;
    }

    [HttpPut("end/{id:guid}")]
    [Authorize]
    public async Task<ActionResult<TimeRecording>> EndRecording(Guid id, DateTime endTime)
    {
        var result = await _service.EndRecording(id, endTime);

        if (result is null)
            return NotFound("Error");

        return result;
    }
    
    // Bei erstellung/starten eines TimeRecordings => Exception
    // Grund: kein Plan wer der User ist
    // Fix: Token für authentifizierung von user. JWT -> wird per POST bei request mitgegeben
    //      Server überprüft Token und gibt die Notwendigen UserInfos weiter
}