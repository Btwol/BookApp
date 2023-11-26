namespace BookApp.Server.Repositories
{
    public class TaggableRepository<T> : BaseRepository<T>, ITaggableRepository<T> where T : class, ITaggable
    {
        public TaggableRepository(DataContext context) : base(context)
        {
        }

        public override IQueryable<T> QueryWithIncludes(DbSet<T> querry)
        {
            return querry.Include(t => t.Tags);
        }
    }
}
