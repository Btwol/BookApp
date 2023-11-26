using BookApp.Shared.Models.Identity;

namespace BookApp.Client.Services.Interfaces
{
    public interface IAppUserClientService
    {
        public Task<AppUserModel> GetUserByEmail(string email);
        public Task<AppUserModel> GetUserById(int userId);
    }
}
