using TimeTracking.Domain;

namespace TimeTracking.Service.Interfaces;

public interface IUserService
{
    User GetById(Guid id);
    User GetByEmail(string email);
    IEnumerable<User> GetAll();
    IEnumerable<User> AddUser(User user);
    IEnumerable<User> UpdateUser(Guid id, User user);
    IEnumerable<User> DeleteUser(Guid id);
    User Login(User user);
    User Register(User user);
}