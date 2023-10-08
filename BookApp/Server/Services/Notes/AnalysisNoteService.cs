namespace BookApp.Server.Services.Notes
{
    public class AnalysisNoteService : NoteService<AnalysisNote, AnalysisNoteModel>, IAnalysisNoteService
    {
        public AnalysisNoteService(IAnalysisNoteMapperService noteMapper, IBookAnalysisRepository bookAnalysisRepository,
            IBaseRepository<AnalysisNote> noteRepository)
            : base(noteMapper, bookAnalysisRepository, noteRepository)
        {
        }
    }
}
