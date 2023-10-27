namespace BookApp.Server.Controllers
{
    [ApiController]
    [JwtAuthorize("User")]
    [Route("[controller]")]
    public class AnalysisNoteController : ControllerBase
    {
        private readonly IAnalysisNoteServerService _analysisNoteService;
        private readonly ITagManagerServerService<AnalysisNote> _tagService;

        public AnalysisNoteController(IAnalysisNoteServerService analysisNoteService, ITagManagerServerService<AnalysisNote> tagService)
        {
            _analysisNoteService = analysisNoteService;
            _tagService = tagService;
        }

        [HttpPost("AddAnalysisNote")]
        public async Task<ServiceResponse> AddAnalysisNote(AnalysisNoteModel analysisNoteModel)
        {
            return await _analysisNoteService.AddNote(analysisNoteModel, analysisNoteModel.BookAnalysisId);
        }

        [HttpDelete("DeleteAnalysisNote/{noteId}/{bookAnalysisId}")]
        public async Task<ServiceResponse> DeleteNote(int noteId, int bookAnalysisId)
        {
            return await _analysisNoteService.DeleteNote(noteId, bookAnalysisId);
        }

        [HttpPut("EditAnalysisNote")]
        public async Task<ServiceResponse> EditNote(AnalysisNoteModel analysisNoteModel)
        {
            return await _analysisNoteService.EditNote(analysisNoteModel, analysisNoteModel.BookAnalysisId);
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
