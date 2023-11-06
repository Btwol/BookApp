namespace BookApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [JwtAuthorize("User")]
    public class BookAnalysisController : ControllerBase
    {
        private readonly IBookAnalysisServerService _bookAnalysisService;

        public BookAnalysisController(IBookAnalysisServerService bookAnalysisService)
        {
            _bookAnalysisService = bookAnalysisService;
        }

        [HttpGet("GetAnalysisById/{bookAnalysisId}")]
        public async Task<ServiceResponse> GetAnalysisById(int bookAnalysisId)
        {
            return await _bookAnalysisService.GetAnalysisById(bookAnalysisId);
        }

        [HttpPost("CreateBookAnalysis")]
        public async Task<ServiceResponse> CreateBookAnalysis([FromBody] BookAnalysisSummaryModel newBookAnalysis)
        {
            return await _bookAnalysisService.CreateBookAnalysis(newBookAnalysis);
        }

        [HttpPut("EditBookAnalysis")]
        public async Task<ServiceResponse> EditBookAnalysis([FromBody] BookAnalysisSummaryModel updatedBookAnalysis)
        {
            return await _bookAnalysisService.EditBookAnalysis(updatedBookAnalysis);
        }

        [HttpGet("GetAnalysesByHash/{bookHash}")]
        public async Task<ServiceResponse> GetAnalysesByHash(string bookHash)
        {
            return await _bookAnalysisService.GetAnalysesByHash(bookHash);
        }

        [HttpDelete("DeleteBookAnalysis/{bookAnalysisId}")]
        public async Task<ServiceResponse> DeleteBookAnalysis(int bookAnalysisId)
        {
            return await _bookAnalysisService.DeleteBookAnalysis(bookAnalysisId);
        }
    }
}
