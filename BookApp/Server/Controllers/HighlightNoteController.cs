using BookApp.Shared.Models.ClientModels.Notes;

namespace BookApp.Server.Controllers
{
    [ApiController]
    [JwtAuthorize("User")]
    [Route("[controller]")]
    public class HighlightNoteController : ControllerBase
    {
        private readonly IHighlightNoteService _highlightNoteService;

        public HighlightNoteController(IHighlightNoteService highlightNoteService)
        {
            _highlightNoteService = highlightNoteService;
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
    }
}
