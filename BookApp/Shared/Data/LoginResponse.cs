namespace BookApp.Shared.Data
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public ApiUserResponseDto User { get; set; }
    }
}
