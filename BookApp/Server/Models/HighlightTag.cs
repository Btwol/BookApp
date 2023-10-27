using System.ComponentModel.DataAnnotations.Schema;

namespace BookApp.Server.Models
{
    public class HighlightTag
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Highlight")]
        public int? HighlightId { get; set; }
        public virtual Highlight? Highlight { get; set; }

        [ForeignKey("Tag")]
        public int? TagId { get; set; }
        public virtual Tag? Tag { get; set; }
    }
}
