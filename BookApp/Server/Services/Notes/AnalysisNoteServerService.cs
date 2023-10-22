using BookApp.Server.Repositories.Interfaces.Notes;

namespace BookApp.Server.Services.Notes
{
    public class AnalysisNoteServerService : NoteServerService<AnalysisNote, AnalysisNoteModel>, IAnalysisNoteServerService
    {
        public AnalysisNoteServerService(IAnalysisNoteMapper analysisNoteMapperService, IBookAnalysisRepository bookAnalysisRepository,
            INoteRepository<AnalysisNote> noteRepository, IBookAnalysisServerService bookAnalysisServerService)
            : base(analysisNoteMapperService, bookAnalysisRepository, noteRepository, bookAnalysisServerService)
        {
        }
    }
}
