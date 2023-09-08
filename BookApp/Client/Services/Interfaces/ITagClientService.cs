namespace BookApp.Client.Services.Interfaces
{
    public interface ITagClientService
    {
        public Task<HttpResponseMessage> AddTag(int tagId, int highlightId);
        public Task<HttpResponseMessage> CreateNewTag();
        public Task<HttpResponseMessage> GetTags(int bookAnalysisId);
    }
}
