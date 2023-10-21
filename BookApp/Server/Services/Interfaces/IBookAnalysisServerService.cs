namespace BookApp.Server.Services.Interfaces
{
    public interface IBookAnalysisServerService
    {
        public Task<ServiceResponse> GetBookAnalysis(int analysisId);
        public Task<ServiceResponse> CreateBookAnalysis(BookAnalysisModel newBookAnalysis);
        public Task<ServiceResponse> EditBookAnalysis(BookAnalysisModel updatedBookAnalysis);
        public Task<ServiceResponse> DeleteBookAnalysis(int analysisId);
        public Task<ServiceResponse> GetAnalysisByHash(string bookHash);
        public Task<bool> CurrentUserIsMemberTypeOfAnalysis(int analysisId, params MemberType[] memberType);
    }
}
