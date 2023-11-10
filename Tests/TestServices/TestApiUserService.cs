namespace Tests.TestServices
{
    internal class TestAppUserService : IAppUserService
    {
        public int GetCurrentUserId()
        {
            return 1;
        }

        public bool CurrentUserIsAdmin()
        {
            return false;
        }
    }
}
