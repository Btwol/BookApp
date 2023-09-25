using BookApp.Shared.Models.ClientModels;
using BookApp.Shared.Models.Services;

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
            return null;// await _highlightService.UpdateHighlight(updatedHighlight);
        }

        [HttpPost("AddHighlight")]
        public async Task<ServiceResponse> AddHighlight([FromBody] HighlightModel newHighlight)
        {
            return await _highlightService.AddHighlight(newHighlight);
        }

        [HttpDelete("DeleteHighlight/{highlightId}")]
        public async Task<ServiceResponse> DeleteHighlight(int highlightId)
        {
            return await _highlightService.DeleteHighlight(highlightId);
        }

    }
}
