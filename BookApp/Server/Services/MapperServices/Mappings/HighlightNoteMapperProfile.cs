namespace BookApp.Server.Services.MapperServices.Mappings
{
    public class HighlightNoteMapperProfile : Profile
    {
        public HighlightNoteMapperProfile()
        {
            CreateMap<HighlightNoteModel, HighlightNote>().ReverseMap();
        }
    }
}