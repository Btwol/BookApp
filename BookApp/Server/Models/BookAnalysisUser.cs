using System.ComponentModel.DataAnnotations.Schema;

namespace BookApp.Server.Models
{
    public class BookAnalysisUser
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("AppUser")]
        public int UsersId { get; set; }
        public virtual AppUser AppUser { get; set; }

        [ForeignKey("BookAnalysis")]
        public int BookAnalysisId { get; set; }
        public virtual BookAnalysis BookAnalysis { get; set; }

        public MemberType MemberType { get; set; }
    }
}
