namespace BookApp.Server.Services.Interfaces.Identity
{
    public interface IAppUserGetterService
    {
        public Task<ServiceResponse> GetUserById(int userId);
        public Task<ServiceResponse> GetUserByEmail(string userEmail);
    }
}
