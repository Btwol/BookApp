namespace BookApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChapterNoteController : ControllerBase
    {
        private readonly IChapterNoteService _chapterNoteService;

        public ChapterNoteController(IChapterNoteService chapterNoteService)
        {
            _chapterNoteService = chapterNoteService;
        }

        [JwtAuthorize("User")]
        [HttpPost("AddChapterNote")]
        public async Task<ServiceResponse> AddChapterNote(ChapterNoteModel chapterNoteModel)
        {
            return await _chapterNoteService.AddNote(chapterNoteModel);
        }
    }

}
