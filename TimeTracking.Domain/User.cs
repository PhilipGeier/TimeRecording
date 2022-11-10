using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TimeTracking.Domain.DataTransferObjects;

namespace TimeTracking.Domain;

public class User
{
    public Guid Id { get; set; }
    
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public string PhoneNumber { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }

    public UserDto ToUserDto()
    {
        return new UserDto
        {
            Id = Id,
            Email = Email,
            FirstName = FirstName,
            LastName = LastName,
            PhoneNumber = PhoneNumber
        };
    }
}