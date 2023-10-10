namespace BookApp.Server.Services.Notes
{
    public class AnalysisNoteService : NoteService<AnalysisNote, AnalysisNoteModel>, IAnalysisNoteService
    {
        public AnalysisNoteService(IAnalysisNoteMapperService analysisNoteMapperService, IBookAnalysisRepository bookAnalysisRepository, 
            IBaseRepository<AnalysisNote> noteRepository, IBookAnalysisServerService bookAnalysisServerService) 
            : base(analysisNoteMapperService, bookAnalysisRepository, noteRepository, bookAnalysisServerService)
        {
        }
    }
}
