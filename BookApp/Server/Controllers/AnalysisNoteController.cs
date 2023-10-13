using BookApp.Shared.Models.ClientModels.Notes;

namespace BookApp.Server.Controllers
{
    [ApiController]
    [JwtAuthorize("User")]
    [Route("[controller]")]
    public class AnalysisNoteController : ControllerBase
    {
        private readonly IAnalysisNoteService _analysisNoteService;
        private readonly ITagServerService<AnalysisNote> _tagService;

        public AnalysisNoteController(IAnalysisNoteService analysisNoteService, ITagServerService<AnalysisNote> tagService)
        {
            _analysisNoteService = analysisNoteService;
            _tagService = tagService;
        }

        [HttpPost("AddAnalysisNote")]
        public async Task<ServiceResponse> AddAnalysisNote(AnalysisNoteModel analysisNoteModel)
        {
            return await _analysisNoteService.AddNote(analysisNoteModel);
        }

        [HttpDelete("DeleteAnalysisNote/{noteId}")]
        public async Task<ServiceResponse> DeleteNote(int noteId)
        {
            return await _analysisNoteService.DeleteNote(noteId);
        }

        [HttpPut("EditAnalysisNote")]
        public async Task<ServiceResponse> EditNote(AnalysisNoteModel analysisNoteModel)
        {
            return await _analysisNoteService.EditNote(analysisNoteModel);
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
