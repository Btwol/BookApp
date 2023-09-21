using BookApp.Client.Services.Interfaces;
using BookApp.Shared.Interfaces.Model;
using BookApp.Shared.Models.ClientModels;
using BookApp.Shared.Models.Services;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using static BookApp.Client.Services.BookAnalysisClientService;

namespace BookApp.Client.Services
{
    public class BookAnalysisClientService : IBookAnalysisClientService
    {
        private readonly HttpClient Http;
        private readonly IJSRuntime jsRuntime;

        public BookAnalysisClientService(HttpClient http, IJSRuntime jSRuntime)
        {
            Http = http;
            this.jsRuntime = jSRuntime;
        }

        public async Task<HttpResponseMessage> CreateBookAnalysis(BookAnalysisModel newBookAnalysis)
        {
            await AddTokenToRequest();
            return await Http.PostAsJsonAsync<BookAnalysisModel>("BookAnalysis/CreateBookAnalysis", newBookAnalysis);
        }

        public async Task<HttpResponseMessage> DeleteBookAnalysis(int bookAnalysisId)
        {
            await AddTokenToRequest();
            return await Http.DeleteAsync($"BookAnalysis/DeleteBookAnalysis/{bookAnalysisId}");
        }

        public async Task<HttpResponseMessage> EditBookAnalysis(BookAnalysisModel updatedBookAnalysis)
        {
            await AddTokenToRequest();
            return await Http.PutAsJsonAsync<BookAnalysisModel>("BookAnalysis/EditBookAnalysis", updatedBookAnalysis);
        }

        public async Task<HttpResponseMessage> GetAnalysisByHash(string bookHash)
        {
            await AddTokenToRequest();
            return await Http.GetAsync($"BookAnalysis/GetAnalysisByHash/{bookHash}");
        }


        private async Task AddTokenToRequest()
        {
            var token = await jsRuntime.InvokeAsync<string>("localStorageFunctions.getItem", "currentUserToken");
            Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
