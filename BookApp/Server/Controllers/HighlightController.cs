using BookApp.Server.Services.Interfaces;
using BookApp.Shared.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HighlightController : ControllerBase
    {
        private readonly IHighlightServerService _highlightService;

        public HighlightController(IHighlightServerService highlightService)
        {
            _highlightService = highlightService;
        }

        [HttpPut("UpdateHighlight")]
        public async Task<ServiceResponse> UpdateHighlight(HighlightModel updatedHighlight)
        {
            return await _highlightService.UpdateHighlight(updatedHighlight);
        }

        [HttpGet("RetrieveHighlights")]
        public async Task<ServiceResponse> RetrieveHighlights(int bookAnalysisId)
        {
            return await _highlightService.RetrieveHighlights(bookAnalysisId);
        }
    }
}
