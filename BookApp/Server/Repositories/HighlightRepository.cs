namespace BookApp.Server.Repositories
{
    public class HighlightRepository : BaseRepository<Highlight>, IHighlightRepository
    {
        public HighlightRepository(DataContext context) : base(context)
        {
        }
    }
}
