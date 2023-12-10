namespace BookApp.Server.Services.Notes
{
    public class ChapterNoteServerService : NoteServerService<ChapterNote, ChapterNoteModel>, IChapterNoteServerService
    {
        public ChapterNoteServerService(IChapterNoteMapper noteMapper, IBookAnalysisRepository bookAnalysisRepository,
            IChapterNoteRepository noteRepository, IBookAnalysisServerService bookAnalysisServerService, IHubServerService hubServerService, ITagRepository tagRepository)
            : base(noteMapper, bookAnalysisRepository, noteRepository, bookAnalysisServerService, hubServerService, tagRepository)
        {
        }
    }
}
