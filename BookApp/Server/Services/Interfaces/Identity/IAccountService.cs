namespace BookApp.Server.Services.Interfaces.Identity
{
    public interface IAccountService
    {
        public Task<ServiceResponse> LoginUser(LoginRequest model);
        public Task<ServiceResponse> RegisterUser(RegisterRequest model);
    }
}
