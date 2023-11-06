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

        public async Task<BookAnalysisSummaryModel> CreateBookAnalysis(BookAnalysisSummaryModel newBookAnalysis)
        {
            var response = await Http.PostAsJsonAsync<BookAnalysisSummaryModel>("BookAnalysis/CreateBookAnalysis", newBookAnalysis);
            return await HelperService.HandleResponse<BookAnalysisSummaryModel>(response);
        }

        public async Task DeleteBookAnalysis(int bookAnalysisId)
        {
            var response = await Http.DeleteAsync($"BookAnalysis/DeleteBookAnalysis/{bookAnalysisId}");
            await HelperService.HandleResponse(response);
        }

        public async Task EditBookAnalysis(BookAnalysisSummaryModel updatedBookAnalysis)
        {
            var response = await Http.PutAsJsonAsync<BookAnalysisSummaryModel>("BookAnalysis/EditBookAnalysis", updatedBookAnalysis);
            await HelperService.HandleResponse(response);
        }

        public async Task<List<BookAnalysisSummaryModel>> GetAnalysesByHash(string bookHash)
        {
            var response = await Http.GetAsync($"BookAnalysis/GetAnalysesByHash/{bookHash}");
            return await HelperService.HandleResponse<List<BookAnalysisSummaryModel>>(response);
        }

        public async Task<BookAnalysisDetailedModel> GetAnalysisById(int bookAnalysisId)
        {
            var response = await Http.GetAsync($"BookAnalysis/GetAnalysisById/{bookAnalysisId}");
            return await HelperService.HandleResponse<BookAnalysisDetailedModel>(response);
        }
    }
}
