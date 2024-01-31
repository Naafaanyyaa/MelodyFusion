namespace MelodyFusion.BLL.Models.Response.BaseResponse
{
    public class BaseResponse
    {
        public string Id { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; } 
        public DateTime? LastModifiedDate { get; set; }
    }
}
