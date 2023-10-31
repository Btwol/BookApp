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