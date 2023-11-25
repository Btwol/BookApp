using BookApp.Server.Repositories.Interfaces.Notes;

namespace BookApp.Server.Services.Notes
{
    public class ChapterNoteServerService : NoteServerService<ChapterNote, ChapterNoteModel>, IChapterNoteServerService
    {
        public ChapterNoteServerService(IChapterNoteMapper noteMapper, IBookAnalysisRepository bookAnalysisRepository, 
            INoteRepository<ChapterNote> noteRepository, IBookAnalysisServerService bookAnalysisServerService, IHubServerService hubServerService) 
            : base(noteMapper, bookAnalysisRepository, noteRepository, bookAnalysisServerService, hubServerService)
        {
        }
    }
}
