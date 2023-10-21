namespace BookApp.Server.Repositories
{
    public class HighlightRepository : BaseRepository<Highlight>, IHighlightRepository
    {
        public HighlightRepository(DataContext context) : base(context)
        {
        }

        public override IQueryable<Highlight> QueryWithIncludes(DbSet<Highlight> querry)
        {
            return querry
                .Include(h => h.Tags)
                .Include(h => h.HighlightNotes);
        }
    }
}
