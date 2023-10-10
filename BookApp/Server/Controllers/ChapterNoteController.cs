using BookApp.Shared.Models.ClientModels.Notes;

namespace BookApp.Server.Controllers
{
    [ApiController]
    [JwtAuthorize("User")]
    [Route("[controller]")]
    public class ChapterNoteController : ControllerBase
    {
        private readonly IChapterNoteService _chapterNoteService;

        public ChapterNoteController(IChapterNoteService chapterNoteService)
        {
            _chapterNoteService = chapterNoteService;
        }

        [HttpPost("AddChapterNote")]
        public async Task<ServiceResponse> AddChapterNote(ChapterNoteModel chapterNoteModel)
        {
            return await _chapterNoteService.AddNote(chapterNoteModel);
        }

        [HttpDelete("DeleteChapterNote/{noteId}")]
        public async Task<ServiceResponse> DeleteChapterNote(int noteId)
        {
            return await _chapterNoteService.DeleteNote(noteId);
        }

        [HttpPut("EditChapterNote")]
        public async Task<ServiceResponse> EditChapterNote(ChapterNoteModel chapterNoteModel)
        {
            return await _chapterNoteService.EditNote(chapterNoteModel);
        }
    }
}
