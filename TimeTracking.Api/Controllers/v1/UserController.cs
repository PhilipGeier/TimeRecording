using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TimeTracking.Domain.Attributes;
using TimeTracking.Domain.DataTransferObjects;
using TimeTracking.Domain.Enums;
using TimeTracking.Service.Interfaces;

namespace TimeTracking.Api.Controllers.v1;

[Route("/api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _service;
    private readonly IConfiguration _configuration;
    
    public UserController(IUserService service, IConfiguration configuration)
    {
        _service = service;
        _configuration = configuration;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
    {
        return Ok(await _service.GetAll());
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetById(Guid id)
    {
        var result = await _service.GetById(id);

        if (result is null) return NotFound("User does not exist");

        return Ok(result);
    }

    [HttpGet("{email}")]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetByEmail(string email)
    {
        var result = await _service.GetByEmail(email);

        if (result is null) return NotFound("User does not exist");

        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult> Register(UserRegisterDto user)
    {
        var userDto = await _service.Register(user);

        if (userDto is null) 
            return StatusCode(409, "User already exists");

        return Ok(userDto);
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UserDto>> Update(Guid id, UserDto user)
    {        
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        if (identity is null)
            return null;

        var userClaims = identity.Claims;

        var isIdGuid = Guid.TryParse(userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out var userId);
        
        if (!isIdGuid)
            return BadRequest("The id is not a Guid");

        if (!userId.Equals(id))
            return Unauthorized("You are not authorized to do this");
        
        var result = await _service.UpdateUser(id, user);

        if (result is null) return NotFound("User does not exist");

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public async Task<ActionResult<IEnumerable<UserDto>>> Delete(Guid id)
    {
        var result = await _service.DeleteUser(id);

        if (result is null) return NotFound("User does not exist");

        return Ok(result);
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        var user = await _service.Login(userLoginDto.Email, userLoginDto.Password);

        if (user is null)
            return Unauthorized("You are not Authorized to do this");

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.GivenName, user.FirstName),
            new Claim(ClaimTypes.Surname, user.LastName),
            new Claim(ClaimTypes.HomePhone, user.PhoneNumber)
        };

        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims,
            expires: DateTime.Now.AddYears(1), signingCredentials: credentials);
        
        return Ok(new JwtSecurityTokenHandler().WriteToken(token));
    }

    [Authorize]
    [HttpGet("current")]
    public async Task<ActionResult<UserDto?>> GetCurrentUser()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        if (identity is null)
            return null;

        var userClaims = identity.Claims;

        var isIdGuid = Guid.TryParse(userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out var id);
        
        if (!isIdGuid)
            return BadRequest("The id is not a Guid");

        var user = await _service.GetById(id);

        if (user is null)
            return NotFound("User does not exist");

        return Ok(user);
    }
}
