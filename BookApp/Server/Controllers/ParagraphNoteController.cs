using BookApp.Shared.Models.ClientModels.Notes;

namespace BookApp.Server.Controllers
{
    [ApiController]
    [JwtAuthorize("User")]
    [Route("[controller]")]
    public class ParagraphNoteController : ControllerBase
    {
        private readonly IParagraphNoteService _paragraphNoteService;
        private readonly ITagServerService<ParagraphNote> _tagService;
        public ParagraphNoteController(IParagraphNoteService paragraphNoteService, ITagServerService<ParagraphNote> tagService)
        {
            _paragraphNoteService = paragraphNoteService;
            _tagService = tagService;
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
