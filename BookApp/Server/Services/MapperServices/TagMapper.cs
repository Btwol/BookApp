namespace BookApp.Server.Services.MapperServices
{
    public class TagMapper : MapperService<Tag, TagModel>, ITagMapper
    {
        public TagMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}
