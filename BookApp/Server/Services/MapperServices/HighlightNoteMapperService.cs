namespace BookApp.Server.Services.MapperServices
{
    public class HighlightNoteMapperService : MapperService<HighlightNote, HighlightNoteModel>, IHighlightNoteMapperService
    {
        public HighlightNoteMapperService(IMapper mapper) : base(mapper)
        {
        }
    }
}
