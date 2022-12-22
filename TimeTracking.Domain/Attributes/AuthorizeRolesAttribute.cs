using Microsoft.AspNetCore.Authorization;
using TimeTracking.Domain.Enums;

namespace TimeTracking.Domain.Attributes;

public class AuthorizeRolesAttribute : AuthorizeAttribute
{
    public AuthorizeRolesAttribute(params UserRole[] roles)
    {
        Roles = string.Join(",", roles);
    }
}