using Microsoft.AspNetCore.Identity;

namespace PetHospital.Data.Entities.Identity;

public class UserRole : IdentityUserRole<string>
{
    public virtual UserDto UserDto { get; set; }

    public virtual RoleDto RoleDto { get; set; } 
}

