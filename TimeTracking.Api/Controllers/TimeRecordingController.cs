using Microsoft.AspNetCore.Mvc;
using TimeTracking.Service.Interfaces;

namespace TimeTracking.Api.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class TimeRecordingController : ControllerBase
{
    private ITimeRecordingService _service;
    
    public TimeRecordingController(ITimeRecordingService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<ActionResult<TimeRecordingController>> GetAll()
    {
        return Ok();
    }
}