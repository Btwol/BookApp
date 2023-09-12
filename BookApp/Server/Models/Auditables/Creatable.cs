using BookApp.Server.Models.Identity;

namespace BookApp.Server.Models.Auditables
{
    public class Creatable : ICreatable
    {
        [Key]
        public int? CreatorId { get; set; }
        public AppUser? Creator { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
