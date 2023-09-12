namespace BookApp.Server.Services.Interfaces.Identity
{
    public interface IApiUserValidatorService
    {
        public Task<ServiceResponse> ValidateRegisterRequest(RegisterRequest registerRequest);
    }
}
