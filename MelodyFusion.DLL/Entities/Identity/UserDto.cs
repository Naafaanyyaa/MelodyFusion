using MelodyFusion.DLL.Entities;
using Microsoft.AspNetCore.Identity;

namespace PetHospital.Data.Entities.Identity;

public class UserDto : IdentityUser
{
    public bool IsBanned { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public virtual List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public virtual List<SubscriptionDto> Subscription { get; set; } = new List<SubscriptionDto>();
}