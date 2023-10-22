namespace BookApp.Server.Controllers
{
    [ApiController]
    [JwtAuthorize("User")]
    [Route("[controller]")]
    public class TagController : ControllerBase
    {
        private readonly ITagServerService<Highlight> _tagService;  //change this

        public TagController(ITagServerService<Highlight> tagService)
        {
            _tagService = tagService;
        }

        [HttpGet("GetTags/{bookAnalysisId}")]
        public async Task<ServiceResponse> GetTags(int bookAnalysisId)
        {
            return await _tagService.GetTags(bookAnalysisId);
        }

        [HttpDelete("DeleteTag/{tagId}")]
        public async Task<ServiceResponse> DeleteTag(int tagId)
        {
            return await _tagService.DeleteTag(tagId);
        }

        [HttpPost("CreateNewTag/{bookAnalysisId}")]
        public async Task<ServiceResponse> CreateNewTag([FromBody] TagModel newTag, int bookAnalysisId)
        {
            return await _tagService.CreateNewTag(newTag, bookAnalysisId);
        }
    }
}
