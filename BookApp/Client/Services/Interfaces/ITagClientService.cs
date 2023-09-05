namespace BookApp.Client.Services.Interfaces
{
    public interface ITagClientService
    {
        public Task<HttpResponseMessage> AddTag();
        public Task<HttpResponseMessage> CreateNewTag();
        public Task<HttpResponseMessage> GetTags(int bookAnalysisId);
    }
}
