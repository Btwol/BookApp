using BookApp.Shared.Models.Enums;
using BookApp.Shared.Models.Services;

namespace BookApp.Client.Services.Interfaces
{
    public interface IAnalysisMembershipClientService
    {
        public Task ChangeMemberStatus(int bookAnalysisId, int memberUserId, MemberType newMemberType);
        public Task InviteUser(int bookAnalysisId, int invitedUserId);
        public Task RemoveUser(int bookAnalysisId, int removedUserId);
        public Task AcceptInvite(int bookAnalysisId);
        public Task DeclineInvite(int bookAnalysisId);
    }
}
