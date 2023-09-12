using BookApp.Client.Services.Interfaces;
using BookApp.Shared.Data;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace BookApp.Client.Services
{
    public class HighlightClientService : IHighlightClientService
    {
        private readonly HttpClient Http;

        public HighlightClientService(HttpClient http)
        {
            Http = http;
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
    }
}
