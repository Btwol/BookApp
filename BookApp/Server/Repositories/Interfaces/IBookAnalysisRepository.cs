namespace BookApp.Server.Repositories.Interfaces
{
    public interface IBookAnalysisRepository : IBaseRepository<BookAnalysis>
    {
        public Task<BookAnalysis> RetrieveDetailedAnalysisById(int bookAnalysisId);
        public Task<IEnumerable<BookAnalysis>> GetAnalysisSummariesByHash(string bookHash);

    }
}
