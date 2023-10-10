using BookApp.Shared.Models.ClientModels.Notes;

namespace BookApp.Server.Controllers
{
    [ApiController]
    [JwtAuthorize("User")]
    [Route("[controller]")]
    public class AnalysisNoteController : ControllerBase
    {
        private readonly IAnalysisNoteService _analysisNoteService;

        public AnalysisNoteController(IAnalysisNoteService analysisNoteService)
        {
            _analysisNoteService = analysisNoteService;
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
    }
}
