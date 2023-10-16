using BookApp.Server.Repositories.Interfaces.Notes;

namespace BookApp.Server.Repositories.Notes
{
    public class HighlightNoteRepository : BaseRepository<HighlightNote>, IHighlightNoteRepository
    {
        public HighlightNoteRepository(DataContext context) : base(context)
        {
        }

        public override IQueryable<HighlightNote> QueryWithIncludes(DbSet<HighlightNote> querry)
        {
            return querry.Include(n => n.Highlight).ThenInclude(h => h.BookAnalysis);
        }
    }
}
