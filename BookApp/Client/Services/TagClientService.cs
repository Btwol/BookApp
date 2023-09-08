using BookApp.Client.Services.Interfaces;

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

        public Task<HttpResponseMessage> CreateNewTag()
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseMessage> GetTags(int bookAnalysisId)
        {
            return await Http.GetAsync($"Tag/GetTags/{bookAnalysisId}");
        }
    }
}
