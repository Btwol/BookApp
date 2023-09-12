namespace BookApp.Server.Repositories
{
    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(DataContext context) : base(context)
        {
        }
    }
}
