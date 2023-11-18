namespace BookApp.Server.Services.Interfaces
{
    public interface IAnalysisMembershipServerService
    {
        public Task<ServiceResponse> ChangeMemeberStatus(int bookAnalysisId, int memberUserId, MemberType newMemberType);
    }
}
