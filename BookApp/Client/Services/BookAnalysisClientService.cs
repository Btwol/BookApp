using BookApp.Client.Services.Interfaces;
using BookApp.Shared.Models.ClientModels;
using BookApp.Shared.Models.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;
using Microsoft.JSInterop;

namespace BookApp.Client.Services
{
    public class BookAnalysisClientService : IBookAnalysisClientService
    {
        private readonly HttpClient Http;
        private readonly IJSRuntime jsRuntime;

        public BookAnalysisClientService(HttpClient http, IJSRuntime jsRuntime, IJSRuntime jSRuntime)
        {
            Http = http;
            this.jsRuntime = jSRuntime;
        }

        public async Task<HttpResponseMessage> CreateBookAnalysis(BookAnalysisModel newBookAnalysis)
        {
            await AddTokenToRequest();

            var response = await Http.PostAsJsonAsync<BookAnalysisModel>("BookAnalysis/CreateBookAnalysis", newBookAnalysis);
            return response;
        }

        public async Task<HttpResponseMessage> GetAnalysisByHash(string bookHash)
        {
            await AddTokenToRequest();

            var response = await Http.GetAsync($"BookAnalysis/GetAnalysisByHash/{bookHash}");
            return response;
        }

        private async Task AddTokenToRequest()
        {
            var token = await jsRuntime.InvokeAsync<string>("localStorageFunctions.getItem", "currentUserToken");
            Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
