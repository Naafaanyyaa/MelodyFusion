namespace MelodyFusion.BLL.Models.Response
{
    public class PhotoResponse : BaseResponse.BaseResponse
    {
        public string Uri { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }
}