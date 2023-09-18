using BookApp.Shared.Models.ClientModels;
using BookApp.Shared.Models.Services;

namespace BookApp.Server.Services.Interfaces
{
    public interface IBookAnalysisServerService
    {
        public Task<ServiceResponse> GetBookAnalysis(int analysisId);
        public Task<ServiceResponse> CreateBookAnalysis(BookAnalysisModel newBookAnalysis);
        public Task<ServiceResponse> UpdateBookAnalysis(BookAnalysisModel updatedBookAnalysis);
        public Task<ServiceResponse> DeleteBookAnalysis(int analysisId);
        public Task<ServiceResponse> GetAnalysisByHash(string bookHash);
    }
}
