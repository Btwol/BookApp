using BookApp.Shared.Models.ClientModels.Notes;

namespace BookApp.Server.Controllers
{
    [ApiController]
    [JwtAuthorize("User")]
    [Route("[controller]")]
    public class ChapterNoteController : ControllerBase
    {
        private readonly IChapterNoteService _chapterNoteService;
        private readonly ITagServerService<ChapterNote> _tagService;

        public ChapterNoteController(IChapterNoteService chapterNoteService, ITagServerService<ChapterNote> tagService)
        {
            _chapterNoteService = chapterNoteService;
            _tagService = tagService;
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
