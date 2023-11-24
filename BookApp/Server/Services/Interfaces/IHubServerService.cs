namespace BookApp.Server.Services.Interfaces
{
    public interface IHubServerService
    {
        public Task AnalysisSummaryUpdated(BookAnalysisSummaryModel bookAnalysisSummaryModel);
    }
}
