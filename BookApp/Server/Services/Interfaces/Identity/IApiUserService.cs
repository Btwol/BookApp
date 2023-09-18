namespace BookApp.Server.Services.Interfaces.Identity
{
    public interface IAppUserService
    {
        public int GetCurrentUserId();
        public bool CurrentUserIsAdmin();
    }
}
