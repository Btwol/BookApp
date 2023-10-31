using BookApp.Server.Models;

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
                .Include(h => h.HighlightNotes).ThenInclude(n => n.Tags);
        }

        public async override Task Delete(Highlight model)
        {
            var highlightToDelete = await FindByConditionsFirstOrDefault(h => h.Id == model.Id);

            highlightToDelete.Tags.Clear();
            foreach(var highlightNote in highlightToDelete.HighlightNotes)
            {
                highlightNote.Tags.Clear();
                _context.Remove(highlightNote);
            }

            _context.Remove(highlightToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
