using BookApp.Server.Models.Identity;
using BookApp.Shared.Models.Identity;

namespace BookApp.Server.Services.MapperServices.Mappings
{
    public class AppUserMapperProfile : Profile
    {
        public AppUserMapperProfile()
        {
            CreateMap<AppUser, AppUserModel>();
            CreateMap<RegisterRequest, AppUser>();
        }
    }
}