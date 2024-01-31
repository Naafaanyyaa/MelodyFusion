using MelodyFusion.DLL.Enums;

namespace MelodyFusion.BLL.Models.Request
{
    public class ChangeRoleRequest
    {
        public string UserId { get; set; } = string.Empty;
        public RoleEnum Role { get; set; }
    }
}
