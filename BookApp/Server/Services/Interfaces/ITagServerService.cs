namespace BookApp.Server.Services.Interfaces
{
    public interface ITagServerService<T> where T : ITaggable
    {
        public Task<ServiceResponse> GetTags(int bookAnalysisId);
        public Task<ServiceResponse> AddTag(int taggedId, int tagId);
        public Task<ServiceResponse> CreateNewTag(TagModel newTag, int bookAnalysisId);
        public Task<ServiceResponse> RemoveTag(int taggedId, int tagId);
        public Task<ServiceResponse> DeleteTag(int tagId);
    }
}
