using BookApp.Client.Services.Interfaces;
using BookApp.Shared.Data;
using System.Net.Http.Json;

namespace BookApp.Client.Services
{
    public class TagClientService : ITagClientService
    {
        private readonly HttpClient Http;

        public TagClientService(HttpClient http)
        {
            Http = http;
        }

        public async Task<HttpResponseMessage> AddTag(int tagId, int highlightId)
        {
            return await Http.PostAsync($"Tag/AddTag/{highlightId}/{tagId}", null);
        }

        public async Task<HttpResponseMessage> CreateNewTag(TagModel newTag, int bookAnalysisId)
        {
            return await Http.PostAsJsonAsync<TagModel>($"Tag/CreateNewTag/{bookAnalysisId}", newTag);
        }

        public async Task<HttpResponseMessage> GetTags(int bookAnalysisId)
        {
            return await Http.GetAsync($"Tag/GetTags/{bookAnalysisId}");
        }
    }
}
