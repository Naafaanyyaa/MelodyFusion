using System.ComponentModel.DataAnnotations;

namespace MelodyFusion.BLL.Models.Request
{
    public class UserRequest
    {
        [Required]
        [StringLength(18, MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(18, MinimumLength = 2)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(18, MinimumLength = 8)]
        public string UserName { get; set; } = string.Empty;

        [Required] 
        [EmailAddress] 
        public string Email { get; set; } = string.Empty;
    }
}
