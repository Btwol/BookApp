namespace BookApp.Server.Controllers
{
    [ApiController]
    [JwtAuthorize("User")]
    [Route("[controller]")]
    public class TagController : ControllerBase
    {
        private readonly ITagServerService _tagService;

        public TagController(ITagServerService tagService)
        {
            _tagService = tagService;
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

        [HttpPut("EditTag")]
        public async Task<ServiceResponse> EditTag([FromBody] TagModel tagToEdit)
        {
            return await _tagService.EditTag(tagToEdit);
        }
    }
}
