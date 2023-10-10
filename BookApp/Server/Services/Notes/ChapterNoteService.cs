namespace BookApp.Server.Services.Notes
{
    public class ChapterNoteService : NoteService<ChapterNote, ChapterNoteModel>, IChapterNoteService
    {
        public ChapterNoteService(IChapterNoteMapperService chapterNoteMapperService, IBookAnalysisRepository bookAnalysisRepository, 
            IBaseRepository<ChapterNote> noteRepository, IBookAnalysisServerService bookAnalysisServerService) 
            : base(chapterNoteMapperService, bookAnalysisRepository, noteRepository, bookAnalysisServerService)
        {
        }
    }
}
