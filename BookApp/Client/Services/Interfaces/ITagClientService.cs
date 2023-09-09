using BookApp.Shared.Data;

namespace BookApp.Client.Services.Interfaces
{
    public interface ITagClientService
    {
        public Task<HttpResponseMessage> AddTag(int tagId, int highlightId);
        public Task<HttpResponseMessage> CreateNewTag(TagModel newTag, int bookAnalysisId);
        public Task<HttpResponseMessage> GetTags(int bookAnalysisId);
    }
}
