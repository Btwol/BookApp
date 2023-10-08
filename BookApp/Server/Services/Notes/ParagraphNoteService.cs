namespace BookApp.Server.Services.Notes
{
    public class ParagraphNoteService : NoteService<ParagraphNote, ParagraphNoteModel>, IParagraphNoteService
    {
        public ParagraphNoteService(IParagraphNoteMapperService noteMapper, IBookAnalysisRepository bookAnalysisRepository,
            IBaseRepository<ParagraphNote> noteRepository)
            : base(noteMapper, bookAnalysisRepository, noteRepository)
        {
        }
    }
}
