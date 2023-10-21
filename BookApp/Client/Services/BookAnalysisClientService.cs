using BookApp.Client.Services.Interfaces;
using BookApp.Shared.Models.ClientModels;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace BookApp.Client.Services
{
    public class BookAnalysisClientService : IBookAnalysisClientService
    {
        private readonly HttpClient Http;

        public BookAnalysisClientService(HttpClient http, IJSRuntime jsRuntime)
        {
            Http = http;
            HelperService.AddTokenToRequest(http, jsRuntime);
        }

        public async Task<HttpResponseMessage> CreateBookAnalysis(BookAnalysisModel newBookAnalysis)
        {
            return await Http.PostAsJsonAsync<BookAnalysisModel>("BookAnalysis/CreateBookAnalysis", newBookAnalysis);
        }

        public async Task<HttpResponseMessage> DeleteBookAnalysis(int bookAnalysisId)
        {
            return await Http.DeleteAsync($"BookAnalysis/DeleteBookAnalysis/{bookAnalysisId}");
        }

        public async Task<HttpResponseMessage> EditBookAnalysis(BookAnalysisModel updatedBookAnalysis)
        {
            return await Http.PutAsJsonAsync<BookAnalysisModel>("BookAnalysis/EditBookAnalysis", updatedBookAnalysis);
        }

        public async Task<HttpResponseMessage> GetAnalysisByHash(string bookHash)
        {
            //await AddTokenToRequest();
            return await Http.GetAsync($"BookAnalysis/GetAnalysisByHash/{bookHash}");
        }
    }
}
