namespace BookApp.Server.Services.Interfaces.Identity
{
    public interface IApiUserGetterService
    {
        public Task<ServiceResponse> GetUserById(int userId);
    }
}
