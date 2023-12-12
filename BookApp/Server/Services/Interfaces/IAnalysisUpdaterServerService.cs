namespace BookApp.Server.Services.Interfaces
{
    public interface IAnalysisUpdaterServerService
    {
        public Task<ServiceResponse> UpdateAnalysisModel(AnalysisVersionModel analysisVersionModel);
    }
}
