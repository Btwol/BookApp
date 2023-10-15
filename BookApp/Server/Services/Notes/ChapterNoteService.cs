using BookApp.Server.Repositories.Interfaces.Notes;

namespace BookApp.Server.Services.Notes
{
    public class ChapterNoteService : NoteService<ChapterNote, ChapterNoteModel>, IChapterNoteService
    {
        public ChapterNoteService(IChapterNoteMapperService chapterNoteMapperService, IBookAnalysisRepository bookAnalysisRepository,
            INoteRepository noteRepository, IBookAnalysisServerService bookAnalysisServerService) 
            : base(chapterNoteMapperService, bookAnalysisRepository, noteRepository, bookAnalysisServerService)
        {
        }
    }
}
