namespace BookApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookAnalysisController : ControllerBase
    {
        private readonly IBookAnalysisServerService _bookAnalysisService;

        public BookAnalysisController(IBookAnalysisServerService bookAnalysisService)
        {
            _bookAnalysisService = bookAnalysisService;
        }

        [HttpGet("GetBookAnalysis")]
        public async Task<ServiceResponse> GetBookAnalysis(int analysisId)
        {
            return await _bookAnalysisService.GetBookAnalysis(analysisId);
        }

        [HttpPost("CreateBookAnalysis")]
        public async Task<ServiceResponse> CreateBookAnalysis([FromBody] BookAnalysisModel newBookAnalysis)
        {
            return await _bookAnalysisService.CreateBookAnalysis(newBookAnalysis);
        }

        [HttpPut("UpdateBookAnalysis")]
        public async Task<ServiceResponse> UpdateBookAnalysis(BookAnalysisModel updatedBookAnalysis)
        {
            return await _bookAnalysisService.UpdateBookAnalysis(updatedBookAnalysis);
        }

        [HttpGet("GetAnalysisByHash/{bookHash}")]
        public async Task<ServiceResponse> GetAnalysisByHash(string bookHash)
        {
            var response = await _bookAnalysisService.GetAnalysisByHash(bookHash);
            return response;
        }
    }
}
