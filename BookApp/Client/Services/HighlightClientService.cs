using BookApp.Client.Services.Interfaces;
using BookApp.Shared.Models.ClientModels;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace BookApp.Client.Services
{
    public class HighlightClientService : IHighlightClientService
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _jsRuntime;

        public HighlightClientService(HttpClient http, IJSRuntime jsRuntime)
        {
            _http = http;
            _jsRuntime = jsRuntime;
        }

        public async Task<HighlightModel> AddHighlight(HighlightModel newHighlight)
        {
            await HelperService.AddTokenToRequest(_http, _jsRuntime);
            var response = await _http.PostAsJsonAsync($"Highlight/AddHighlight", newHighlight);
            return await HelperService.HandleResponse<HighlightModel>(response);
        }

        public async Task DeleteHighlight(int highlightId)
        {
            await HelperService.AddTokenToRequest(_http, _jsRuntime);
            var response = await _http.DeleteAsync($"Highlight/DeleteHighlight/{highlightId}");
            await HelperService.HandleResponse(response);
        }

        public async Task UpdateHighlight(HighlightModel updatedHighlight)
        {
            await HelperService.AddTokenToRequest(_http, _jsRuntime);
            var response = await _http.PutAsJsonAsync($"Highlight/UpdateHighlight", updatedHighlight);
            await HelperService.HandleResponse(response);
        }

        public async Task AddTag(int highlightId, int tagId)
        {
            await HelperService.AddTokenToRequest(_http, _jsRuntime);
            var response = await _http.PostAsync($"Highlight/AddTag/{highlightId}/{tagId}", null);
            await HelperService.HandleResponse(response);
        }

        public async Task RemoveTag(int highlightId, int tagId)
        {
            await HelperService.AddTokenToRequest(_http, _jsRuntime);
            var response = await _http.DeleteAsync($"Highlight/RemoveTag/{highlightId}/{tagId}");
            await HelperService.HandleResponse(response);
        }
    }
}
