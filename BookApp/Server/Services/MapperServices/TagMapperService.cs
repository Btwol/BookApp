namespace BookApp.Server.Services.MapperServices
{
    public class TagMapperService : MapperService<Tag, TagModel>, ITagMapperService
    {
        public TagMapperService(IMapper mapper) : base(mapper)
        {
        }
    }
}
