using TimeTracking.Domain;
using TimeTracking.Service.Interfaces;

namespace TimeTracking.Service;

public class UserService : IUserService
{
    public User GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public User GetByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<User> GetAll()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<User> AddUser(User user)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<User> UpdateUser(Guid id, User user)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<User> DeleteUser(Guid id)
    {
        throw new NotImplementedException();
    }

    public User Login(User user)
    {
        throw new NotImplementedException();
    }

    public User Register(User user)
    {
        throw new NotImplementedException();
    }
}