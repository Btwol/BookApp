﻿namespace BookApp.Server.Controllers
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

        [HttpPut("EditBookAnalysis")]
        public async Task<ServiceResponse> EditBookAnalysis([FromBody] BookAnalysisModel updatedBookAnalysis)
        {
            if (updatedBookAnalysis.AnalysisTitle == "rush")
            {
                throw new Exception("test EditBookAnalysis exception!");
            }

            return await _bookAnalysisService.EditBookAnalysis(updatedBookAnalysis);
        }

        [HttpGet("GetAnalysisByHash/{bookHash}")]
        public async Task<ServiceResponse> GetAnalysisByHash(string bookHash)
        {
            return await _bookAnalysisService.GetAnalysisByHash(bookHash);
        }

        [HttpDelete("DeleteBookAnalysis/{bookAnalysisId}")]
        public async Task<ServiceResponse> DeleteBookAnalysis(int bookAnalysisId)
        {
            return await _bookAnalysisService.DeleteBookAnalysis(bookAnalysisId);
        }
    }
}
