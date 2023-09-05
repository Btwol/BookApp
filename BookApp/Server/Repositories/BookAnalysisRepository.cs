namespace BookApp.Server.Repositories
{
    public class BookAnalysisRepository : BaseRepository<BookAnalysis>, IBookAnalysisRepository
    {
        public BookAnalysisRepository(DataContext context) : base(context)
        {
           
        }
        public virtual async Task<IEnumerable<BookAnalysis>> FindByConditions(Expression<Func<BookAnalysis, bool>> expresion)
        {
            return await _context.Set<BookAnalysis>()
                .Where(expresion)
                .Include(b => b.Highlights)
                .Include(b => b.Tags)
                .ToListAsync();
        }
    }
}
