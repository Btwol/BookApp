using BookApp.Shared.Enums;

namespace BookApp.Server.Services.Interfaces
{
    public interface IAnalysisMembershipServerService
    {
        public Task<ServiceResponse> ChangeMemberStatus(int bookAnalysisId, int memberUserId, MemberType newMemberType);
        public Task<ServiceResponse> InviteUser(int bookAnalysisId, int invitedUserId);
        public Task<ServiceResponse> RemoveUser(int bookAnalysisId, int removedUserId);
        public Task<ServiceResponse> AcceptInvite(int bookAnalysisId);
        public Task<ServiceResponse> DeclineInvite(int bookAnalysisId);
    }
}
