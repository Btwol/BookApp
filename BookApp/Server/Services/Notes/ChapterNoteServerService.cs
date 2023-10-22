using BookApp.Server.Repositories.Interfaces.Notes;

namespace BookApp.Server.Services.Notes
{
    public class ChapterNoteServerService : NoteServerService<ChapterNote, ChapterNoteModel>, IChapterNoteServerService
    {
        public ChapterNoteServerService(IChapterNoteMapper chapterNoteMapperService, IBookAnalysisRepository bookAnalysisRepository,
            INoteRepository<ChapterNote> noteRepository, IBookAnalysisServerService bookAnalysisServerService)
            : base(chapterNoteMapperService, bookAnalysisRepository, noteRepository, bookAnalysisServerService)
        {
        }
    }
}
