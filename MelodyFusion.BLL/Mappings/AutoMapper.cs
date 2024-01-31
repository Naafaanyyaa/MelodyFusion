using AutoMapper;
using Braintree;
using MelodyFusion.BLL.Models.Request;
using MelodyFusion.BLL.Models.Response;
using MelodyFusion.DLL.Entities;
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
            CreateMap<SubscriptionDto, SubscriptionResponse>();
            CreateMap<Result<Transaction>, SubscriptionDto>()
                .ForMember(x => x.Amount, o => o.MapFrom(s => s.Transaction.Amount));
        }
    }
}