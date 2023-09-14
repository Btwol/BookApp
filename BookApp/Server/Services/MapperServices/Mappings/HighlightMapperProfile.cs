using BookApp.Shared.Models.ClientModels;

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