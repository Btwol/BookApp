using BookApp.Server.Models.Identity;

namespace BookApp.Server.Services.Interfaces.Identity
{
    public interface IApiUserMapperService
    {
        public Task<ApiUserResponseDto> MapGetApiUserResponseDto(AppUser user);
        public LoginResponse MapUserLoginResponse(AppUser user, string token);
        public AppUser MapUserRegisterRequest(RegisterRequest user);
    }
}
