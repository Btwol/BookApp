using BookApp.Client.Services.Interfaces;
using BookApp.Shared.Models.ClientModels;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace BookApp.Client.Services
{
    public class BookAnalysisClientService : IBookAnalysisClientService
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _jsRuntime;

        public BookAnalysisClientService(HttpClient http, IJSRuntime jsRuntime)
        {
            _http = http;
            _jsRuntime = jsRuntime;
        }

        public async Task<BookAnalysisSummaryModel> CreateBookAnalysis(BookAnalysisSummaryModel newBookAnalysis)
        {
            await HelperService.AddTokenToRequest(_http, _jsRuntime);
            var response = await _http.PostAsJsonAsync("BookAnalysis/CreateBookAnalysis", newBookAnalysis);
            return await HelperService.HandleResponse<BookAnalysisSummaryModel>(response);
        }

        public async Task DeleteBookAnalysis(int bookAnalysisId)
        {
            await HelperService.AddTokenToRequest(_http, _jsRuntime);
            var response = await _http.DeleteAsync($"BookAnalysis/DeleteBookAnalysis/{bookAnalysisId}");
            await HelperService.HandleResponse(response);
        }

        public async Task EditBookAnalysis(BookAnalysisSummaryModel updatedBookAnalysis)
        {
            await HelperService.AddTokenToRequest(_http, _jsRuntime);
            var response = await _http.PutAsJsonAsync<BookAnalysisSummaryModel>("BookAnalysis/EditBookAnalysis", updatedBookAnalysis);
            await HelperService.HandleResponse(response);
        }

        public async Task<List<BookAnalysisSummaryModel>> GetAnalysesByHash(string bookHash)
        {
            await HelperService.AddTokenToRequest(_http, _jsRuntime);
            var response = await _http.GetAsync($"BookAnalysis/GetAnalysesByHash/{bookHash}");
            return await HelperService.HandleResponse<List<BookAnalysisSummaryModel>>(response);
        }

        public async Task<BookAnalysisDetailedModel> GetAnalysisById(int bookAnalysisId)
        {
            await HelperService.AddTokenToRequest(_http, _jsRuntime);
            var response = await _http.GetAsync($"BookAnalysis/GetAnalysisById/{bookAnalysisId}");
            return await HelperService.HandleResponse<BookAnalysisDetailedModel>(response);
        }
    }
}
