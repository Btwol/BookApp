namespace BookApp.Server.Controllers
{
    [ApiController]
    [JwtAuthorize("User")]
    [Route("[controller]")]
    public class ChapterNoteController : ControllerBase
    {
        private readonly IChapterNoteServerService _chapterNoteService;
        private readonly ITagManagerServerService<ChapterNote> _tagService;

        public ChapterNoteController(IChapterNoteServerService chapterNoteService, ITagManagerServerService<ChapterNote> tagService)
        {
            _chapterNoteService = chapterNoteService;
            _tagService = tagService;
        }

        [HttpPost("AddChapterNote")]
        public async Task<ServiceResponse> AddChapterNote(ChapterNoteModel chapterNoteModel)
        {
            return await _chapterNoteService.AddNote(chapterNoteModel, chapterNoteModel.BookAnalysisId);
        }

        [HttpDelete("DeleteChapterNote/{noteId}/{bookAnalysisId}")]
        public async Task<ServiceResponse> DeleteNote(int noteId, int bookAnalysisId)
        {
            return await _chapterNoteService.DeleteNote(noteId, bookAnalysisId);
        }

        [HttpPut("EditChapterNote")]
        public async Task<ServiceResponse> EditNote(ChapterNoteModel chapterNoteModel)
        {
            return await _chapterNoteService.EditNote(chapterNoteModel, chapterNoteModel.BookAnalysisId);
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
