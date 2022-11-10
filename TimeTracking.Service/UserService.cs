
using Microsoft.EntityFrameworkCore;
using TimeTracking.Data;
using TimeTracking.Domain;
using TimeTracking.Service.Interfaces;

namespace TimeTracking.Service;

public class UserService : IUserService
{
    private readonly DataContext _context;

    public UserService(DataContext context)
    {
        _context = context;
    }
    
    public async Task<User?> GetById(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> CreateUser(User user)
    {
        try
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
        catch
        {
            return null;
        }
    }

    public async Task<User?> UpdateUser(Guid id, User request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (user is null) return null;

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;
        user.PhoneNumber = request.PhoneNumber;
        user.Password = request.Password;

        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<IEnumerable<User>?> DeleteUser(Guid id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (user is null) return null;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return await _context.Users.ToListAsync();
    }

    public Task<User> Login(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> Register(User user)
    {
        throw new NotImplementedException();
    }
}