namespace BookApp.Server.Services.MapperServices.Mappings
{
    public class ChapterNoteMapperProfile : Profile
    {
        public ChapterNoteMapperProfile()
        {
            CreateMap<ChapterNoteModel, ChapterNote>().ReverseMap();
        }
    }

}
