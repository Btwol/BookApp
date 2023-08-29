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
            var response = await Http.PostAsJsonAsync($"Highlight/AddHighlight", newHighlight);
            string contentString = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Response Content:");
            Console.WriteLine(contentString);
            //Console.WriteLine("ADD HIGHLIGHT RESPONSE CONTENT: " + response.IsSuccessStatusCode + "\n" + response.Headers);
            return response;
        }

        public Task<ServiceResponse> UpdateHighlight(HighlightModel updatedHighlight)
        {
            throw new NotImplementedException();
        }
    }
}
