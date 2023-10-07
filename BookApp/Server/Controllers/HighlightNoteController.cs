namespace BookApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HighlightNoteController : ControllerBase
    {
        private readonly IHighlightNoteService _highlightNoteService;

        public HighlightNoteController(IHighlightNoteService highlightNoteService)
        {
            _highlightNoteService = highlightNoteService;
        }

        [JwtAuthorize("User")]
        [HttpPost("AddHighlightNote")]
        public async Task<ServiceResponse> AddHighlightNote(HighlightNoteModel highlightNoteModel)
        {
            return await _highlightNoteService.AddNote(highlightNoteModel);
        }
    }
}
