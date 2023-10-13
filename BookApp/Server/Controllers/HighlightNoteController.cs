using BookApp.Shared.Models.ClientModels.Notes;

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
            return await _highlightNoteService.AddNote(highlightNoteModel);
        }

        [HttpDelete("DeleteHighlightNote/{noteId}")]
        public async Task<ServiceResponse> DeleteHighlightNote(int noteId)
        {
            return await _highlightNoteService.DeleteNote(noteId);
        }

        [HttpPut("EditHighlightNote")]
        public async Task<ServiceResponse> EditHighlightNote(HighlightNoteModel highlightNoteModel)
        {
            return await _highlightNoteService.EditNote(highlightNoteModel);
        }

        [HttpPost("AddTag/{noteId}/{tagId}")]
        public async Task<ServiceResponse> AddTag(int noteId, int tagId)
        {
            return await _tagService.AddTag(noteId, tagId);
        }

        [HttpDelete("RemoveTag/{noteId}")]
        public async Task<ServiceResponse> RemoveTag(int noteId, int tagId)
        {
            return await _tagService.RemoveTag(noteId, tagId);
        }

    }
}
