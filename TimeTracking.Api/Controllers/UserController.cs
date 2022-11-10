using Microsoft.AspNetCore.Mvc;
using TimeTracking.Domain;
using TimeTracking.Service.Interfaces;

namespace TimeTracking.Api.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAll()
    {
        return Ok(await _service.GetAll());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<User>> GetById(Guid id)
    {
        var result = await _service.GetById(id);

        if (result is null) return NotFound("User does not exist");

        return Ok(result);
    }

    [HttpGet("{email}")]
    public async Task<ActionResult<User>> GetByEmail(string email)
    {
        var result = await _service.GetByEmail(email);

        if (result is null) return NotFound("User does not exist");

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<User>> Create(User user)
    {
        var result = await _service.CreateUser(user);

        if (user is null) return Forbid("User does already exist");

        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<User>> Update(Guid id, User user)
    {
        var result = await _service.UpdateUser(id, user);

        if (result is null) return NotFound("User does not exist");

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<IEnumerable<User>>> Delete(Guid id)
    {
        var result = await _service.DeleteUser(id);

        if (result is null) return NotFound("User does not exist");

        return Ok(result);
    }

}
