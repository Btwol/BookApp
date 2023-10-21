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
            return await _paragraphNoteService.AddNote(paragraphNoteModel, paragraphNoteModel.BookAnalysisId);
        }

        [HttpDelete("DeleteParagraphNote/{noteId}/{bookAnalysisId}")]
        public async Task<ServiceResponse> DeleteNote(int noteId, int bookAnalysisId)
        {
            return await _paragraphNoteService.DeleteNote(noteId, bookAnalysisId);
        }

        [HttpPut("EditParagraphNote")]
        public async Task<ServiceResponse> EditNote(ParagraphNoteModel paragraphNoteModel)
        {
            return await _paragraphNoteService.EditNote(paragraphNoteModel, paragraphNoteModel.BookAnalysisId);
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
