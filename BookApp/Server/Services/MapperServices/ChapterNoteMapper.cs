namespace BookApp.Server.Services.MapperServices
{
    public class ChapterNoteMapper : MapperService<ChapterNote, ChapterNoteModel>, IChapterNoteMapper
    {
        public ChapterNoteMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}
