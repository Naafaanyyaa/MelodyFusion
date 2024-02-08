using Microsoft.AspNetCore.Identity;

namespace MelodyFusion.DLL.Entities.Identity;

public class UserDto : IdentityUser
{
    public bool IsBanned { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public virtual List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public virtual List<SubscriptionDto>? Subscription { get; set; }
    public virtual PhotoDto? Photo { get; set; }
    public virtual List<AuthenticationStatisticDto>? AuthenticationStatistic { get; set; }
}