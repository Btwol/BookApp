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

        public Task<HttpResponseMessage> AddTag()
        {
            throw new NotImplementedException();
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
