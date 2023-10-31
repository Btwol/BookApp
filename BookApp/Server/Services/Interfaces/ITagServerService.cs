namespace BookApp.Server.Services.Interfaces
{
    public interface ITagServerService
    {
        public Task<ServiceResponse> CreateNewTag(TagModel newTag, int bookAnalysisId);
        public Task<ServiceResponse> EditTag(TagModel editedTag);
        public Task<ServiceResponse> DeleteTag(int tagId);
    }
}
