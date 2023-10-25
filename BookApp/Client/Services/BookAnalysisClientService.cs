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

        public async Task<BookAnalysisModel> CreateBookAnalysis(BookAnalysisModel newBookAnalysis)
        {
            var response = await Http.PostAsJsonAsync<BookAnalysisModel>("BookAnalysis/CreateBookAnalysis", newBookAnalysis);
            return await HelperService.HandleResponse<BookAnalysisModel>(response);
        }

        public async Task DeleteBookAnalysis(int bookAnalysisId)
        {
            var response = await Http.DeleteAsync($"BookAnalysis/DeleteBookAnalysis/{bookAnalysisId}");
            await HelperService.HandleResponse(response);
        }

        public async Task EditBookAnalysis(BookAnalysisModel updatedBookAnalysis)
        {
            var response = await Http.PutAsJsonAsync<BookAnalysisModel>("BookAnalysis/EditBookAnalysis", updatedBookAnalysis);
            await HelperService.HandleResponse(response);
        }

        public async Task<List<BookAnalysisModel>> GetAnalysisByHash(string bookHash)
        {
            var response = await Http.GetAsync($"BookAnalysis/GetAnalysisByHash/{bookHash}");
            return await HelperService.HandleResponse<List<BookAnalysisModel>>(response);
        }
    }
}
