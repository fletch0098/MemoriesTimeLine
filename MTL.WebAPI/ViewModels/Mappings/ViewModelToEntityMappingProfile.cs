using MTL.DataAccess.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;


namespace MTL.WebAPI.ViewModels.Mappings
{
    public class ViewModelToEntityMappingProfile : Profile
    {
        public ViewModelToEntityMappingProfile()
        {
            CreateMap<RegistrationViewModel, IdentityUser>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
        }
    }
}