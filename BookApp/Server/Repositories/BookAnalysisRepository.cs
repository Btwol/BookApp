namespace BookApp.Server.Repositories
{
    public class BookAnalysisRepository : BaseRepository<BookAnalysis>, IBookAnalysisRepository
    {
        public BookAnalysisRepository(DataContext context) : base(context)
        {

        }

        public override IQueryable<BookAnalysis> QueryWithIncludes(DbSet<BookAnalysis> querry)
        {
            return querry.Include(b => b.Highlights).ThenInclude(h => h.Tags)
                .Include(b => b.Highlights).ThenInclude(h => h.HighlightNotes).ThenInclude(n => n.Tags)
                .Include(b => b.Tags)
                .Include(b => b.ParagraphNotes).ThenInclude(n => n.Tags)
                .Include(b => b.ChapterNotes).ThenInclude(n => n.Tags)
                .Include(b => b.AnalysisNotes).ThenInclude(n => n.Tags);
        }
    }
}
