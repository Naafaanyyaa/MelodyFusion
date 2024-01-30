using AutoMapper;
using MelodyFusion.BLL.Models.Request;
using MelodyFusion.BLL.Models.Response;
using PetHospital.Data.Entities.Identity;

namespace MelodyFusion.BLL.Mappings
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<RegistrationRequest, UserDto>()
                .MaxDepth(1);
            CreateMap<UserDto, RegistrationResponse>();
            CreateMap<UserDto, UserResponse>();
            CreateMap<UserRequest, UserDto>();
        }
    }
}