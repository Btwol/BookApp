namespace BookApp.Server.Services.Interfaces
{
    public interface ITagServerService
    {
        public Task<ServiceResponse> GetTags(int bookAnalysisId);
        public Task<ServiceResponse> AddTag(int highlightId, int tagId);
    }
}
