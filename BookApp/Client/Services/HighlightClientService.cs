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

        public async Task<HttpResponseMessage> AddHighlight(HighlightModel newHighlight)
        {
            return await Http.PostAsJsonAsync($"Highlight/AddHighlight", newHighlight);
        }

        public async Task<HttpResponseMessage> DeleteHighlight(int highlightId)
        {
            return await Http.DeleteAsync($"Highlight/DeleteHighlight/{highlightId}");
        }

        public Task<ServiceResponse> UpdateHighlight(HighlightModel updatedHighlight)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseMessage> AddTag(int highlightId, int tagId)
        {
            return await Http.PostAsync($"Highlight/AddTag/{highlightId}/{tagId}", null);
        }

        public async Task<HttpResponseMessage> RemoveTag(int highlightId, int tagId)
        {
            return await Http.DeleteAsync($"Highlight/RemoveTag/{highlightId}/{tagId}");
        }
    }
}
