using BookApp.Shared.Models.ClientModels;

namespace BookApp.Server.Services.MapperServices.Mappings
{
    public class TagMapperProfile : Profile
    {
        public TagMapperProfile()
        {
            CreateMap<TagModel, Tag>().ReverseMap();
        }
    }
}