namespace BookApp.Server.Services.MapperServices.Mappings
{
    public class HighlightMapperProfile : Profile
    {
        public HighlightMapperProfile()
        {
            CreateMap<HighlightModel, Highlight>().ReverseMap();
        }
    }
}