using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using TimeTracking.Domain.DataTransferObjects;
using TimeTracking.Domain.Enums;

namespace TimeTracking.Domain;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PhoneNumber { get; set; }
 
    public string Email { get; set; }

    public string Password { get; set; }
    

    public UserRole Role { get; set; } = UserRole.Standard;

}