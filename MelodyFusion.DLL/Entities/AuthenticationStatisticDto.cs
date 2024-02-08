using MelodyFusion.DLL.Entities.Abstract;
using MelodyFusion.DLL.Entities.Identity;

namespace MelodyFusion.DLL.Entities
{
    public class AuthenticationStatisticDto : BaseEntity
    {
        public AuthenticationStatisticDto()
        {

        }
        public AuthenticationStatisticDto(bool isAuthenticated, string userId)
        {
            UserId = userId;
            IsAuthenticated = isAuthenticated;
        }
        public bool IsAuthenticated { get; set; } = false;
        public string UserId { get; set; } = string.Empty;
        public virtual UserDto User { get; set; }
    }
}
