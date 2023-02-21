using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TimeTracking.Data;
using TimeTracking.Domain;
using TimeTracking.Domain.DataTransferObjects;
using TimeTracking.Service.Interfaces;

namespace TimeTracking.Service;

public class UserService : IUserService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public UserService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserDto?> GetById(Guid id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user is null)
            return null;

        var userDto = _mapper.Map<UserDto>(user);

        return userDto;
    }

    public async Task<UserDto?> GetByEmail(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

        if (user is null)
            return null;

        var userDto = _mapper.Map<UserDto>(user);
        
        return userDto;
    }

    public async Task<IEnumerable<UserDto>> GetAll()
    {
        var users = await _context.Users.ToListAsync();

        var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
        return userDtos;
    }

    public async Task<UserDto?> Register(UserRegisterDto request)
    {
        if (await _context.Users.AnyAsync(x => x.Email == request.Email))
            return null;

        var user = _mapper.Map<User>(request);

        var salt = BCrypt.Net.BCrypt.GenerateSalt(10);

        user.Id = Guid.NewGuid();
        user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        var userDto = _mapper.Map<UserDto>(user);
        return userDto;
    }

    public async Task<UserDto?> UpdateUser(Guid id, UserDto request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (user is null) return null;

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;
        user.PhoneNumber = request.PhoneNumber;
        user.Role = request.Role;

        await _context.SaveChangesAsync();

        var userDto = _mapper.Map<UserDto>(user);
        
        return userDto; 
    }

    public async Task<List<UserDto>?> DeleteUser(Guid id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (user is null) return null;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        var users = _context.Users.ToListAsync();
        var userDtos = _mapper.Map<List<UserDto>>(users);

        return userDtos;
    }
    
    public async Task<UserDto?> Login(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x =>
            x.Email == email);
        
        if (user is null)
            return null;
        
        var passwordMatches = BCrypt.Net.BCrypt.Verify(password, user.Password);

        if (!passwordMatches)
            return null;
        
        var userDto = _mapper.Map<UserDto>(user);
        return userDto;
    }
}