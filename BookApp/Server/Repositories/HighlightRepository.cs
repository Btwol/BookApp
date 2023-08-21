using BookApp.Server.Database;
using BookApp.Server.Models;
using BookApp.Server.Repositories.Interfaces;

namespace BookApp.Server.Repositories
{
    public class HighlightRepository : BaseRepository<Highlight>, IHighlightRepository
    {
        public HighlightRepository(DataContext context) : base(context)
        {
        }
    }
}
