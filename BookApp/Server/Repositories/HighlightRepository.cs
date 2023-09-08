using System.Linq;

namespace BookApp.Server.Repositories
{
    public class HighlightRepository : BaseRepository<Highlight>, IHighlightRepository
    {
        public HighlightRepository(DataContext context) : base(context)
        {
        }

        public override async Task<Highlight> FindByConditionsFirstOrDefault(Expression<Func<Highlight, bool>> expresion)
        {
            return await _context.Set<Highlight>()
                .Where(expresion)
                .Include(h => h.Tags)
                .FirstOrDefaultAsync();
        }
    }
}
