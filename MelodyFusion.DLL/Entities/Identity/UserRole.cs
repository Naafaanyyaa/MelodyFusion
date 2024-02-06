using Microsoft.AspNetCore.Identity;

namespace MelodyFusion.DLL.Entities.Identity;

public class UserRole : IdentityUserRole<string>
{
    public virtual UserDto UserDto { get; set; }

    public virtual RoleDto RoleDto { get; set; } 
}

