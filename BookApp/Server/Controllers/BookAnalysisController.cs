using BookApp.Server.Attributes;
using BookApp.Shared.Models.ClientModels;
using BookApp.Shared.Models.Services;

namespace BookApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookAnalysisController : ControllerBase
    {
        private readonly IBookAnalysisServerService _bookAnalysisService;
        IApiUserService _userService;

        public BookAnalysisController(IBookAnalysisServerService bookAnalysisService, IApiUserService userService)
        {
            _bookAnalysisService = bookAnalysisService;
            _userService = userService;
        }

        [HttpGet("GetBookAnalysis")]
        public async Task<ServiceResponse> GetBookAnalysis(int analysisId)
        {
            return await _bookAnalysisService.GetBookAnalysis(analysisId);
        }

        [JwtAuthorize("User")]
        [HttpPost("CreateBookAnalysis")]
        public async Task<ServiceResponse> CreateBookAnalysis([FromBody] BookAnalysisModel newBookAnalysis)
        {
            var resp =  await _bookAnalysisService.CreateBookAnalysis(newBookAnalysis);
            return resp;
        }

        [HttpPut("UpdateBookAnalysis")]
        public async Task<ServiceResponse> UpdateBookAnalysis(BookAnalysisModel updatedBookAnalysis)
        {
            return await _bookAnalysisService.UpdateBookAnalysis(updatedBookAnalysis);
        }

        [JwtAuthorize("User")]
        [HttpGet("GetAnalysisByHash/{bookHash}")]
        public async Task<ServiceResponse> GetAnalysisByHash(string bookHash)
        {
            var id = _userService.GetCurrentUserId();
            var response = await _bookAnalysisService.GetAnalysisByHash(bookHash);
            return response;
        }
    }
}
