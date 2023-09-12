namespace BookApp.Server.Services.Interfaces.Identity
{
    public interface IApiUserService
    {
        public int GetCurrentUserId();
        public bool CurrentUserIsAdmin();
    }
}
