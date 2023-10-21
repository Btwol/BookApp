namespace BookApp.Server.Models.Auditables
{
    public class Creatable : ICreatable
    {
        public int? CreatorId { get; set; }
        public AppUser? Creator { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
