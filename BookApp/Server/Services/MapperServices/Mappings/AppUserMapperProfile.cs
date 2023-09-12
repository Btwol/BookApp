using BookApp.Server.Models.Identity;

namespace BookApp.Server.Services.MapperServices.Mappings
{
    public class AppUserMapperProfile : Profile
    {
        public AppUserMapperProfile()
        {
            CreateMap<AppUser, ApiUserResponseDto>();
            CreateMap<RegisterRequest, AppUser>();
        }
    }
}