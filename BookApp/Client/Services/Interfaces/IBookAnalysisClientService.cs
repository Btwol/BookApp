using BookApp.Shared.Models.ClientModels;

namespace BookApp.Client.Services.Interfaces
{
    public interface IBookAnalysisClientService
    {
        //public Task<ServiceResponse> GetBookAnalysis(int analysisId);
        public Task<HttpResponseMessage> CreateBookAnalysis(BookAnalysisModel newBookAnalysis);
        public Task<HttpResponseMessage> UpdateBookAnalysis(BookAnalysisModel updatedBookAnalysis);
        public Task<HttpResponseMessage> DeleteBookAnalysis(int bookAnalysisId);
        public Task<HttpResponseMessage> GetAnalysisByHash(string bookHash);
    }
}
