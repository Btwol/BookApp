using BookApp.Shared.Models.Identity;
using BookApp.Shared.Models.Services;

namespace BookApp.Server.Services.Interfaces.Identity
{
    public interface IAppUserValidatorService
    {
        public Task<ServiceResponse> ValidateRegisterRequest(RegisterRequest registerRequest);
    }
}
