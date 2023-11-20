using BookApp.Client.Services.Interfaces;
using BookApp.Shared.Models.Identity;
using Microsoft.JSInterop;

namespace BookApp.Client.Services
{
    public class AppUserClientService : IAppUserClientService
    {
        private readonly HttpClient Http;

        public AppUserClientService(HttpClient http, IJSRuntime jsRuntime)
        {
            Http = http;
            HelperService.AddTokenToRequest(http, jsRuntime);
        }

        public async Task<AppUserModel> GetUserByEmail(string email)
        {
            var response = await Http.GetAsync($"Users/GetUserByEmail/{email}");
            return await HelperService.HandleResponse<AppUserModel>(response);
        }
    }
}
