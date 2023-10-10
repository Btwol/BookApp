using BookApp.Shared.Models.ClientModels.Notes;

namespace BookApp.Server.Controllers
{
    [ApiController]
    [JwtAuthorize("User")]
    [Route("[controller]")]
    public class ParagraphNoteController : ControllerBase
    {
        private readonly IParagraphNoteService _paragraphNoteService;

        public ParagraphNoteController(IParagraphNoteService paragraphNoteService)
        {
            _paragraphNoteService = paragraphNoteService;
        }

        [HttpPost("AddParagraphNote")]
        public async Task<ServiceResponse> AddParagraphNote(ParagraphNoteModel paragraphNoteModel)
        {
            return await _paragraphNoteService.AddNote(paragraphNoteModel);
        }

        [HttpDelete("DeleteParagraphNote/{noteId}")]
        public async Task<ServiceResponse> DeleteParagraphNote(int noteId)
        {
            return await _paragraphNoteService.DeleteNote(noteId);
        }

        [HttpPut("EditParagraphNote")]
        public async Task<ServiceResponse> EditParagraphNote(ParagraphNoteModel paragraphNoteModel)
        {
            return await _paragraphNoteService.EditNote(paragraphNoteModel);
        }
    }
}
