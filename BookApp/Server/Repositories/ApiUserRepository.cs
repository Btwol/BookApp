using BookApp.Server.Models.Identity;

namespace BookApp.Server.Repositories
{
    public class ApiUserRepository : BaseRepository<AppUser>, IApiUserRepository
    {
        public ApiUserRepository(DataContext context) : base(context) { }
    }
}
