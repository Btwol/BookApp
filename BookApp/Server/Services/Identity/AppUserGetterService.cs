using BookApp.Shared.Models.Identity;

namespace BookApp.Server.Services.Identity
{
    public class AppUserGetterService : IAppUserGetterService
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IAppUserMapperService _appUserMapperService;

        public AppUserGetterService(IAppUserRepository appUserRepository, IAppUserMapperService appUserMapperService)
        {
            _appUserRepository = appUserRepository;
            _appUserMapperService = appUserMapperService;
        }

        public async Task<ServiceResponse> GetUserByEmail(string userEmail)
        {
            var user = await _appUserRepository.FindByConditionsFirstOrDefault(u => u.Email == userEmail);
            return await GetUser(user);
        }

        public async Task<ServiceResponse> GetUserById(int userId)
        {
            var user = await _appUserRepository.FindByConditionsFirstOrDefault(u => u.Id == userId);
            return await GetUser(user);
        }

        private async Task<ServiceResponse> GetUser(AppUser user)
        {
            if (user == null)
            {
                return ServiceResponse.Error($"User not found", HttpStatusCode.NotFound);
            }

            var mappedUser = await _appUserMapperService.MapGetApiUserResponseDto(user);
            return ServiceResponse<AppUserModel>.Success(mappedUser, "User retrieved.");
        }
    }
}
