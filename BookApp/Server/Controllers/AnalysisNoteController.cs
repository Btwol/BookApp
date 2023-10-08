namespace BookApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnalysisNoteController : ControllerBase
    {
        private readonly IAnalysisNoteService _analysisNoteService;

        public AnalysisNoteController(IAnalysisNoteService analysisNoteService)
        {
            _analysisNoteService = analysisNoteService;
        }

        [JwtAuthorize("User")]
        [HttpPost("AddAnalysisNote")]
        public async Task<ServiceResponse> AddAnalysisNote(AnalysisNoteModel analysisNoteModel)
        {
            return await _analysisNoteService.AddNote(analysisNoteModel);
        }
    }
}
