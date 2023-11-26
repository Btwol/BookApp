using BookApp.Shared.Enums;

namespace BookApp.Server.Services.Interfaces
{
    public interface IBookAnalysisServerService
    {
        public Task<ServiceResponse> GetAnalysisById(int analysisId);
        public Task<ServiceResponse> CreateBookAnalysis(BookAnalysisSummaryModel newBookAnalysis);
        public Task<ServiceResponse> EditBookAnalysis(BookAnalysisSummaryModel updatedBookAnalysis);
        public Task<ServiceResponse> DeleteBookAnalysis(int analysisId);
        public Task<ServiceResponse> GetAnalysesByHash(string bookHash);
        public Task<bool> CurrentUserIsMemberTypeOfAnalysis(int analysisId, params MemberType[] memberType);
    }
}
