using BookApp.Client.Services.Interfaces;
using BookApp.Shared.Models.Identity;
using Microsoft.JSInterop;

namespace BookApp.Client.Services
{
    public class AppUserClientService : IAppUserClientService
    {
        private readonly HttpClient http;
        private readonly IJSRuntime _jsRuntime;

        public AppUserClientService(HttpClient http, IJSRuntime jsRuntime)
        {
            this.http = http;
            _jsRuntime = jsRuntime;
        }

        public async Task<AppUserModel> GetUserByEmail(string email)
        {
            await HelperService.AddTokenToRequest(http, _jsRuntime);
            var response = await http.GetAsync($"Users/GetUserByEmail/{email}");
            return await HelperService.HandleResponse<AppUserModel>(response);
        }

        public async Task<AppUserModel> GetUserById(int userId)
        {
            await HelperService.AddTokenToRequest(http, _jsRuntime);
            var response = await http.GetAsync($"Users/GetUserById/{userId}");
            return await HelperService.HandleResponse<AppUserModel>(response);
        }
    }
}
