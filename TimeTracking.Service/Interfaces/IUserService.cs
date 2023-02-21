using TimeTracking.Domain;
using TimeTracking.Domain.DataTransferObjects;

namespace TimeTracking.Service.Interfaces;

public interface IUserService
{
    Task<UserDto?> GetById(Guid id);
    Task<UserDto?> GetByEmail(string email);
    Task<IEnumerable<UserDto>> GetAll();
    Task<UserDto?> Register(UserRegisterDto request);
    Task<UserDto?> UpdateUser(Guid id, UserDto request);
    Task<List<UserDto>?> DeleteUser(Guid id);
    Task<UserDto?> Login(string email, string password);

}