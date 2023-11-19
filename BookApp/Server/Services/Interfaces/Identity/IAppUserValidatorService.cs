using BookApp.Shared.Models.Identity;

namespace BookApp.Server.Services.Interfaces.Identity
{
    public interface IAppUserValidatorService
    {
        public Task<ServiceResponse> ValidateRegisterRequest(RegisterRequest registerRequest);
    }
}
