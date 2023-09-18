namespace BookApp.Server.Repositories
{
    public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(DataContext context) : base(context) { }

        public async override Task<AppUser> FindByConditionsFirstOrDefault(Expression<Func<AppUser, bool>> expresion)
        {
            return await _context.Set<AppUser>().Where(expresion)
            .Include(i => i.BookAnalyses)
            .FirstOrDefaultAsync();
        }
    }
}
