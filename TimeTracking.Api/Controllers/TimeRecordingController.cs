using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using TimeTracking.Domain;
using TimeTracking.Service.Interfaces;

namespace TimeTracking.Api.Controllers;

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
    public async Task<ActionResult<IEnumerable<TimeRecording>>> GetAll()
    {
        return Ok(await _service.GetAll());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TimeRecording>> GetById(Guid id)
    {
        var result = await _service.GetById(id);
        
        if (result is null) return NotFound("Time Recording does not exist");

        return Ok(result);
    }

    [HttpGet("user/{id:guid}")]
    public async Task<ActionResult<IEnumerable<TimeRecording>>> GetByUser(Guid id)
    {
        var result = await _service.GetByUser(id);

        if (result is null) return NotFound("Time Recording does not exist");

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<TimeRecording>> Create(TimeRecording recording)
    {
        var result = await _service.CreateTimeRecording(recording);

        if (result is null) return Forbid("Time Recording already exists");

        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<TimeRecording>> Update(Guid id, TimeRecording timeRecording)
    {
        var result = await _service.UpdateTimeRecording(id, timeRecording);

        if (result is null) return NotFound("Time Recording does not exist");

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<IEnumerable<TimeRecording>>> Delete(Guid id)
    {
        var result = await _service.DeleteTimeRecording(id);

        if (result is null) return NotFound("Time Recording does not exist");

        return Ok(result);
    }

}