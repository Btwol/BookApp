using BookApp.Shared.Models.Identity;
using BookApp.Shared.Models.Services;

namespace BookApp.Server.Services.Interfaces.Identity
{
    public interface IApiUserValidatorService
    {
        public Task<ServiceResponse> ValidateRegisterRequest(RegisterRequest registerRequest);
    }
}
