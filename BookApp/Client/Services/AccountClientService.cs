using BookApp.Client.Services.Interfaces;
using BookApp.Shared.Models.Identity;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace BookApp.Client.Services
{
    public class AccountClientService : IAccountClientService
    {
        private readonly HttpClient _http;
        private readonly IAppStorage _appStorage;
        private readonly IHubClientService _hubClientService;
        public AccountClientService(HttpClient http, IAppStorage appStorage, IHubClientService hubClientService)
        {
            _http = http;
            _appStorage = appStorage;
            _hubClientService = hubClientService;
        }

        public async Task Login(LoginRequest loginRequest)
        {
            var response = await _http.PostAsJsonAsync<LoginRequest>($"Account/Login", loginRequest);
            var user = await HelperService.HandleResponse<LoginResponse>(response);
            await _appStorage.StoreUser(user);
        }

        public async Task Logout()
        {
            await _hubClientService.LeaveAnalysisEditGroup();
            await _appStorage.DeleteUserFromStorage();
            await _appStorage.DeleteAnalysisFromStorage();
            await _appStorage.DeleteBookFromStorage();
        }

        public async Task Register(RegisterRequest registerRequest)
        {
            var response = await _http.PostAsJsonAsync<RegisterRequest>($"Account/Register", registerRequest);
            await HelperService.HandleResponse(response);
        }
    }
}
