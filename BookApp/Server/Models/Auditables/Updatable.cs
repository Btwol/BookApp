using BookApp.Server.Models.Identity;

namespace BookApp.Server.Models.Auditables
{
    public class Updatable : Creatable
    {
        [Key]
        public int? UpdaterId { get; set; }
        public virtual AppUser? Updater { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
