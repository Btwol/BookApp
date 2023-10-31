using BookApp.Server.Repositories.Interfaces.Notes;

namespace BookApp.Server.Services.Notes
{
    public class ParagraphNoteServerService : NoteServerService<ParagraphNote, ParagraphNoteModel>, IParagraphNoteServerService
    {
        public ParagraphNoteServerService(IParagraphNoteMapper paragraphNoteMapperService, IBookAnalysisRepository bookAnalysisRepository,
            INoteRepository<ParagraphNote> noteRepository, IBookAnalysisServerService bookAnalysisServerService)
            : base(paragraphNoteMapperService, bookAnalysisRepository, noteRepository, bookAnalysisServerService)
        {
        }
    }
}
