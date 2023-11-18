using BookApp.Server.Models;

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

        [HttpPost("ChangeMemeberStatus/{bookAnalysisId}/{memberUserId}")]
        public async Task<ServiceResponse> ChangeMemeberStatus(int bookAnalysisId, int memberUserId, MemberType newMemberType)
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
    }
}