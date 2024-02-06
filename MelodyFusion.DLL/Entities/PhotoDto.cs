using MelodyFusion.DLL.Entities.Identity;
using PetHospital.Data.Entities.Abstract;

namespace MelodyFusion.DLL.Entities
{
    public class PhotoDto : BaseEntity
    {
        public string Uri { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public virtual UserDto User{ get; set; }
    }
}
