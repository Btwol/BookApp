using BookApp.Server.Services.Interfaces.Identity;
using BookApp.Shared.Models.Identity;
using BookApp.Shared.Models.Services;

namespace BookApp.Server.Services.Identity
{
    public class ApiUserGetterService : IApiUserGetterService
    {
        private readonly IApiUserRepository _apiUserRepository;
        private readonly IApiUserMapperService _apiUserMapperService;

        public ApiUserGetterService(IApiUserRepository apiUserRepository, IApiUserMapperService apiUserMapperService)
        {
            _apiUserRepository = apiUserRepository;
            _apiUserMapperService = apiUserMapperService;
        }

        public async Task<ServiceResponse> GetUserById(int userId)
        {
            var user = await _apiUserRepository.FindByConditionsFirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return ServiceResponse.Error($"User with id:{userId} not found", HttpStatusCode.NotFound);
            }

            return ServiceResponse<AppUserModel>.Success(await _apiUserMapperService.MapGetApiUserResponseDto(user), "User retrieved.");
        }
    }
}
