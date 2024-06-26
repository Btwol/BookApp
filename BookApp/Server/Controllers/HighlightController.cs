﻿namespace BookApp.Server.Controllers
{
    [ApiController]
    [JwtAuthorize("User")]
    [Route("[controller]")]
    public class HighlightController : ControllerBase
    {
        private readonly IHighlightServerService _highlightService;
        private readonly ITagManagerServerService<Highlight> _tagService;

        public HighlightController(IHighlightServerService highlightService, ITagManagerServerService<Highlight> tagService)
        {
            _highlightService = highlightService;
            _tagService = tagService;
        }

        [HttpPost("AddTag/{highlightId}/{tagId}")]
        public async Task<ServiceResponse> AddTag(int highlightId, int tagId)
        {
            return await _tagService.AddTag(highlightId, tagId);
        }

        [HttpPut("UpdateHighlight")]
        public async Task<ServiceResponse> UpdateHighlight(HighlightModel updatedHighlight)
        {
            return await _highlightService.UpdateHighlight(updatedHighlight);
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

        [HttpDelete("RemoveTag/{highlightId}/{tagId}")]
        public async Task<ServiceResponse> RemoveTag(int highlightId, int tagId)
        {
            return await _tagService.RemoveTag(highlightId, tagId);
        }
    }
}
