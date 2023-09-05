using BookApp.Shared.Interfaces.Services;

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
    }
}
