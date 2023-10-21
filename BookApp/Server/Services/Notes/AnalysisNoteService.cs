using BookApp.Server.Repositories.Interfaces.Notes;

namespace BookApp.Server.Services.Notes
{
    public class AnalysisNoteService : NoteService<AnalysisNote, AnalysisNoteModel>, IAnalysisNoteService
    {
        public AnalysisNoteService(IAnalysisNoteMapperService analysisNoteMapperService, IBookAnalysisRepository bookAnalysisRepository,
            INoteRepository<AnalysisNote> noteRepository, IBookAnalysisServerService bookAnalysisServerService)
            : base(analysisNoteMapperService, bookAnalysisRepository, noteRepository, bookAnalysisServerService)
        {
        }
    }
}
