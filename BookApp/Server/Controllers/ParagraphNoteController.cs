namespace BookApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParagraphNoteController : ControllerBase
    {
        private readonly IParagraphNoteService _paragraphNoteService;

        public ParagraphNoteController(IParagraphNoteService paragraphNoteService)
        {
            _paragraphNoteService = paragraphNoteService;
        }

        [JwtAuthorize("User")]
        [HttpPost("AddParagraphNote")]
        public async Task<ServiceResponse> AddParagraphNote(ParagraphNoteModel paragraphNoteModel)
        {
            return await _paragraphNoteService.AddNote(paragraphNoteModel);
        }
    }

}
