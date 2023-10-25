using BookApp.Shared.Models.ClientModels;

namespace BookApp.Client.Services.Interfaces
{
    public interface IBookAnalysisClientService
    {
        public Task<BookAnalysisModel> CreateBookAnalysis(BookAnalysisModel newBookAnalysis);
        public Task EditBookAnalysis(BookAnalysisModel updatedBookAnalysis);
        public Task DeleteBookAnalysis(int bookAnalysisId);
        public Task<List<BookAnalysisModel>> GetAnalysisByHash(string bookHash);
    }
}
