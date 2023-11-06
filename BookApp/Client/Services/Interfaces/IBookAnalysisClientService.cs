using BookApp.Shared.Models.ClientModels;

namespace BookApp.Client.Services.Interfaces
{
    public interface IBookAnalysisClientService
    {
        public Task<BookAnalysisSummaryModel> CreateBookAnalysis(BookAnalysisSummaryModel newBookAnalysis);
        public Task EditBookAnalysis(BookAnalysisSummaryModel updatedBookAnalysis);
        public Task DeleteBookAnalysis(int bookAnalysisId);
        public Task<List<BookAnalysisSummaryModel>> GetAnalysesByHash(string bookHash);
        public Task<BookAnalysisDetailedModel> GetAnalysisById(int bookAnalysisId);
    }
}
