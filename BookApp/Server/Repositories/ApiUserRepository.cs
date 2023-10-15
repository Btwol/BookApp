namespace BookApp.Server.Repositories
{
    public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(DataContext context) : base(context) { }

        public override IQueryable<AppUser> QueryWithIncludes(DbSet<AppUser> querry)
        {
            return querry.Include(i => i.BookAnalyses);
        }
    }
}
