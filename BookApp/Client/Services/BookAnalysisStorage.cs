using BookApp.Client.Services.Interfaces;
using BookApp.Shared.Models.ClientModels;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;

namespace BookApp.Client.Services
{
    public class BookAnalysisStorage : IBookAnalysisStorage
    {
        private readonly IJSRuntime _jSRuntime;
        private const string bookAnalysisKey = "storedBookAnalysis";
        private readonly IBookAnalysisClientService _bookAnalysisClientService;

        public BookAnalysisStorage(IJSRuntime jSRuntime, IBookAnalysisClientService bookAnalysisClientService)
        {
            _jSRuntime = jSRuntime;
            _bookAnalysisClientService = bookAnalysisClientService;
        }

        public async Task<BookAnalysisDetailedModel> GetLoadedBookAnalysis()
        {
            var bookAnalysisId = await _jSRuntime.InvokeAsync<string>("localStorageFunctions.getItem", bookAnalysisKey);
            if (bookAnalysisId is null) throw new Exception("Book analysis not loaded.");
            return await _bookAnalysisClientService.GetAnalysisById(int.Parse(bookAnalysisId));
        }

        public async Task SetBookAnalysis(BookAnalysisDetailedModel bookAnalysis)
        {
            await _jSRuntime.InvokeVoidAsync("localStorageFunctions.setItem", bookAnalysisKey, bookAnalysis.Id);
        }

        public async Task<bool> AnalysisIsLoaded()
        {
            var bookAnalysis = await _jSRuntime.InvokeAsync<string>("localStorageFunctions.getItem", bookAnalysisKey);
            if (bookAnalysis is null) return false;
            else return true;
        }

    }
}
