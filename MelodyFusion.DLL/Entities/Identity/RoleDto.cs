using Microsoft.AspNetCore.Identity;

namespace PetHospital.Data.Entities.Identity;

public class RoleDto : IdentityRole
{
    public RoleDto() : base() { } 

    public RoleDto(string roleName) : base(roleName) { }

    public virtual List<UserRole> UserRoles { get; set; }
}