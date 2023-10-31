using BookApp.Client.Services.Interfaces;
using BookApp.Shared.Models.ClientModels;
using BookApp.Shared.Models.Services;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace BookApp.Client.Services
{
    public class HighlightClientService : IHighlightClientService
    {
        private readonly HttpClient Http;

        public HighlightClientService(HttpClient http, IJSRuntime jsRuntime)
        {
            Http = http;
            HelperService.AddTokenToRequest(http, jsRuntime);
        }

        public async Task<HighlightModel> AddHighlight(HighlightModel newHighlight)
        {
            var response = await Http.PostAsJsonAsync($"Highlight/AddHighlight", newHighlight);
            return await HelperService.HandleResponse<HighlightModel>(response);
        }

        public async Task DeleteHighlight(int highlightId)
        {
            var response = await Http.DeleteAsync($"Highlight/DeleteHighlight/{highlightId}");
            await HelperService.HandleResponse(response);
        }

        public async Task UpdateHighlight(HighlightModel updatedHighlight)
        {
            var response = await Http.PutAsJsonAsync($"Highlight/UpdateHighlight", updatedHighlight);
            await HelperService.HandleResponse(response);
        }

        public async Task AddTag(int highlightId, int tagId)
        {
            var response = await Http.PostAsync($"Highlight/AddTag/{highlightId}/{tagId}", null);
            await HelperService.HandleResponse(response);
        }

        public async Task RemoveTag(int highlightId, int tagId)
        {
            var response = await Http.DeleteAsync($"Highlight/RemoveTag/{highlightId}/{tagId}");
            await HelperService.HandleResponse(response);
        }
    }
}
