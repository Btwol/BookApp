namespace BookApp.Server.Services.Notes
{
    public class ChapterNoteService : NoteService<ChapterNote, ChapterNoteModel>, IChapterNoteService
    {
        public ChapterNoteService(IChapterNoteMapperService noteMapper, IBookAnalysisRepository bookAnalysisRepository,
            IBaseRepository<ChapterNote> noteRepository)
            : base(noteMapper, bookAnalysisRepository, noteRepository)
        {
        }
    }
}
