namespace BookApp.Server.Services.MapperServices
{
    public class ParagraphNoteMapperService : MapperService<ParagraphNote, ParagraphNoteModel>, IParagraphNoteMapperService
    {
        public ParagraphNoteMapperService(IMapper mapper) : base(mapper)
        {
        }
    }
}
