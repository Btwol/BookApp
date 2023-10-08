namespace BookApp.Server.Services.MapperServices
{
    public class ChapterNoteMapperService : MapperService<ChapterNote, ChapterNoteModel>, IChapterNoteMapperService
    {
        public ChapterNoteMapperService(IMapper mapper) : base(mapper)
        {
        }
    }
}
