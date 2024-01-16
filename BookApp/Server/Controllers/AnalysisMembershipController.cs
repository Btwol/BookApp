namespace BookApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [JwtAuthorize("User")]
    public class AnalysisMembershipController
    {
        private readonly IAnalysisMembershipServerService _analysisMembershipService;

        public AnalysisMembershipController(IAnalysisMembershipServerService analysisMembershipService)
        {
            _analysisMembershipService = analysisMembershipService;
        }

        [HttpPost("ChangeMemberStatus/{bookAnalysisId}/{memberUserId}/{newMemberType}")]
        public async Task<ServiceResponse> ChangeMemberStatus(int bookAnalysisId, int memberUserId, MemberType newMemberType)
        {
            return await _analysisMembershipService.ChangeMemberStatus(bookAnalysisId, memberUserId, newMemberType);
        }

        [HttpPost("InviteUser/{bookAnalysisId}/{invitedUserId}")]
        public async Task<ServiceResponse> InviteUser(int bookAnalysisId, int invitedUserId)
        {
            return await _analysisMembershipService.InviteUser(bookAnalysisId, invitedUserId);
        }

        [HttpDelete("RemoveUser/{bookAnalysisId}/{removedUserId}")]
        public async Task<ServiceResponse> RemoveUser(int bookAnalysisId, int removedUserId)
        {
            return await _analysisMembershipService.RemoveUser(bookAnalysisId, removedUserId);
        }

        [HttpPost("AcceptInvite/{bookAnalysisId}")]
        public async Task<ServiceResponse> AcceptInvite(int bookAnalysisId)
        {
            return await _analysisMembershipService.AcceptInvite(bookAnalysisId);
        }

        [HttpDelete("DeclineInvite/{bookAnalysisId}")]
        public async Task<ServiceResponse> DeclineInvite(int bookAnalysisId)
        {
            return await _analysisMembershipService.DeclineInvite(bookAnalysisId);
        }
    }
}