namespace BookApp.Server.Services.Interfaces
{
    public interface ITagServerService
    {
        public Task<ServiceResponse> GetTags(int bookAnalysisId);
    }
}
