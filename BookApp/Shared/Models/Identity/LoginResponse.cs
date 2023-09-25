namespace BookApp.Shared.Models.Identity
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public AppUserModel User { get; set; }
    }
}
