namespace BookApp.Server.Services.MapperServices
{
    public class HighlightNoteMapper : MapperService<HighlightNote, HighlightNoteModel>, IHighlightNoteMapper
    {
        public HighlightNoteMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}
