using BookApp.Server.Repositories.Interfaces.Notes;

namespace BookApp.Server.Services.Notes
{
    public class AnalysisNoteServerService : NoteServerService<AnalysisNote, AnalysisNoteModel>, IAnalysisNoteServerService
    {
        public AnalysisNoteServerService(IAnalysisNoteMapper noteMapper, IBookAnalysisRepository bookAnalysisRepository, 
            INoteRepository<AnalysisNote> noteRepository, IBookAnalysisServerService bookAnalysisServerService, IHubServerService hubServerService) 
            : base(noteMapper, bookAnalysisRepository, noteRepository, bookAnalysisServerService, hubServerService)
        {
        }
    }
}
