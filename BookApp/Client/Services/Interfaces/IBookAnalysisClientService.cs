using BookApp.Shared.Data;
using BookApp.Shared.Interfaces.Services;

namespace BookApp.Client.Services.Interfaces
{
    public interface IBookAnalysisClientService
    {
        public Task<ServiceResponse> GetBookAnalysis(int analysisId);
        public Task<ServiceResponse> CreateBookAnalysis(BookAnalysisModel newBookAnalysis);
        public Task<ServiceResponse> UpdateBookAnalysis(BookAnalysisModel updatedBookAnalysis);
        public Task<HttpResponseMessage> GetAnalysisByHash(string bookHash);
        public Task Crash();
    }
}
