namespace BookApp.Shared.Models.Identity
{
    public class AppUserModel : IResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<string> UserRoles { get; set; } = new();
    }
}
