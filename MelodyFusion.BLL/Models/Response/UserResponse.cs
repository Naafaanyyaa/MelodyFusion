
namespace MelodyFusion.BLL.Models.Response
{
    public class UserResponse : BaseResponse.BaseResponse
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public bool IsBanned { get; set; } = false;
        public PhotoResponse? Photo { get; set; }
        public List<SubscriptionResponse>? SubscriptionList { get; set; }
    }
}
