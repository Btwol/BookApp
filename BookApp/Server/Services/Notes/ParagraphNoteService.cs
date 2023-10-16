using BookApp.Server.Repositories.Interfaces.Notes;

namespace BookApp.Server.Services.Notes
{
    public class ParagraphNoteService : NoteService<ParagraphNote, ParagraphNoteModel>, IParagraphNoteService
    {
        public ParagraphNoteService(IParagraphNoteMapperService paragraphNoteMapperService, IBookAnalysisRepository bookAnalysisRepository,
            INoteRepository<ParagraphNote> noteRepository, IBookAnalysisServerService bookAnalysisServerService) 
            : base(paragraphNoteMapperService, bookAnalysisRepository, noteRepository, bookAnalysisServerService)
        {
        }
    }
}
