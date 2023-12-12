using BookApp.Shared.Models.ClientModels;

namespace BookApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [JwtAuthorize("User")]
    public class AnalysisUpdaterController
    {
        private readonly IAnalysisUpdaterServerService _analysisUpdaterServerService;

        public AnalysisUpdaterController(IAnalysisUpdaterServerService analysisUpdaterServerService)
        {
            _analysisUpdaterServerService = analysisUpdaterServerService;
        }

        [HttpPost("UpdateAnalysisModel")]
        public async Task<ServiceResponse> UpdateAnalysisModel(AnalysisVersionModel analysisVersionModel)
        {
            return await _analysisUpdaterServerService.UpdateAnalysisModel(analysisVersionModel);
        }
    }
}
