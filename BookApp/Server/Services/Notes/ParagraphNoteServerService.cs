namespace BookApp.Server.Services.Notes
{
    public class ParagraphNoteServerService : NoteServerService<ParagraphNote, ParagraphNoteModel>, IParagraphNoteServerService
    {
        public ParagraphNoteServerService(IParagraphNoteMapper noteMapper, IBookAnalysisRepository bookAnalysisRepository,
            IParagraphNoteRepository noteRepository, IBookAnalysisServerService bookAnalysisServerService, IHubServerService hubServerService, ITagRepository tagRepository)
            : base(noteMapper, bookAnalysisRepository, noteRepository, bookAnalysisServerService, hubServerService, tagRepository)
        {
        }
    }
}
