using BookApp.Shared.Models.ClientModels;
using BookApp.Shared.Models.Services;

namespace BookApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagController : ControllerBase
    {
        private readonly ITagServerService _tagService;

        public TagController(ITagServerService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet("GetTags/{bookAnalysisId}")]
        public async Task<ServiceResponse> GetTags(int bookAnalysisId)
        {
            return await _tagService.GetTags(bookAnalysisId);
        }

        [HttpPost("AddTag/{highlightId}/{tagId}")]
        public async Task<ServiceResponse> AddTag(int highlightId, int tagId)
        {
            return await _tagService.AddTag(highlightId, tagId);
        }

        [HttpDelete("RemoveTag/{highlightId}/{tagId}")]
        public async Task<ServiceResponse> RemoveTag(int highlightId, int tagId)
        {
            return await _tagService.RemoveTag(highlightId, tagId);
        }

        [HttpPost("CreateNewTag/{bookAnalysisId}")]
        public async Task<ServiceResponse> CreateNewTag([FromBody]TagModel newTag, int bookAnalysisId)
        {
            return await _tagService.CreateNewTag(newTag, bookAnalysisId);
        }
    }
}
