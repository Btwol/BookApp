using BookApp.Server.Database;
using BookApp.Server.Models;
using BookApp.Server.Repositories.Interfaces;

namespace BookApp.Server.Repositories
{
    public class BookAnalysisRepository : BaseRepository<BookAnalysis>, IBookAnalysisRepository
    {
        public BookAnalysisRepository(DataContext context) : base(context)
        {
        }
    }
}
