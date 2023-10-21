namespace BookApp.Server.Controllers
{
    [ApiController]
    [JwtAuthorize("User")]
    [Route("[controller]")]
    public class HighlightNoteController : ControllerBase
    {
        private readonly IHighlightNoteService _highlightNoteService;
        private readonly ITagServerService<HighlightNote> _tagService;

        public HighlightNoteController(IHighlightNoteService highlightNoteService, ITagServerService<HighlightNote> tagService)
        {
            _highlightNoteService = highlightNoteService;
            _tagService = tagService;
        }

        [HttpPost("AddHighlightNote")]
        public async Task<ServiceResponse> AddHighlightNote(HighlightNoteModel highlightNoteModel)
        {
            return await _highlightNoteService.AddNote(highlightNoteModel, highlightNoteModel.BookAnalysisId);
        }

        [HttpDelete("DeleteHighlightNote/{noteId}/{bookAnalysisId}")]
        public async Task<ServiceResponse> DeleteNote(int noteId, int bookAnalysisId)
        {
            return await _highlightNoteService.DeleteNote(noteId, bookAnalysisId);
        }

        [HttpPut("EditHighlightNote")]
        public async Task<ServiceResponse> EditNote(HighlightNoteModel highlightNoteModel)
        {
            return await _highlightNoteService.EditNote(highlightNoteModel, highlightNoteModel.BookAnalysisId);
        }


        [HttpPost("AddTag/{noteId}/{tagId}")]
        public async Task<ServiceResponse> AddTag(int noteId, int tagId)
        {
            return await _tagService.AddTag(noteId, tagId);
        }

        [HttpDelete("RemoveTag/{noteId}/{tagId}")]
        public async Task<ServiceResponse> RemoveTag(int noteId, int tagId)
        {
            return await _tagService.RemoveTag(noteId, tagId);
        }

    }
}
