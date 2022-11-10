using TimeTracking.Domain;

namespace TimeTracking.Service.Interfaces;

public interface IUserService
{
    Task<User?> GetById(Guid id);
    Task<User?> GetByEmail(string email);
    Task<IEnumerable<User>> GetAll();
    Task<User?> CreateUser(User user);
    Task<User?> UpdateUser(Guid id, User request);
    Task<IEnumerable<User>?> DeleteUser(Guid id);
    Task<User> Login(User user);
    Task<User> Register(User user);
}