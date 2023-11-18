namespace BookApp.Server.Repositories
{
    public class BookAnalysisUserRepository : BaseRepository<BookAnalysisUser>, IBookAnalysisUserRepository
    {
        public BookAnalysisUserRepository(DataContext context) : base(context)
        {
        }

        public override IQueryable<BookAnalysisUser> QueryWithIncludes(DbSet<BookAnalysisUser> querry)
        {
            return querry
                .Include(q => q.AppUser)
                .Include(q => q.BookAnalysis);
        }
    }
}
