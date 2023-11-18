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
            return await _analysisMembershipService.ChangeMemeberStatus(bookAnalysisId, memberUserId, newMemberType);
        }
    }
}