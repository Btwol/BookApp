namespace BookApp.Server.Services.MapperServices
{
    public class ParagraphNoteMapper : MapperService<ParagraphNote, ParagraphNoteModel>, IParagraphNoteMapper
    {
        public ParagraphNoteMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}
