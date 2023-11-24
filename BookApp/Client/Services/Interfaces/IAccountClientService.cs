using BookApp.Shared.Models.Identity;

namespace BookApp.Client.Services.Interfaces
{
    public interface IAccountClientService
    {
        public Task Login(LoginRequest loginRequest);
        public Task Logout();
    }
}
