namespace BookApp.Server.Services.Notes
{
    public class ParagraphNoteService : NoteService<ParagraphNote, ParagraphNoteModel>, IParagraphNoteService
    {
        public ParagraphNoteService(IParagraphNoteMapperService paragraphNoteMapperService, IBookAnalysisRepository bookAnalysisRepository,
            IBaseRepository<ParagraphNote> noteRepository, IBookAnalysisServerService bookAnalysisServerService) 
            : base(paragraphNoteMapperService, bookAnalysisRepository, noteRepository, bookAnalysisServerService)
        {
        }
    }
}
