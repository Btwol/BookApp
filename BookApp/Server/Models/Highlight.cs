using Microsoft.EntityFrameworkCore.Update.Internal;

namespace BookApp.Server.Models
{
    public class Highlight : IDbModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PageNumber { get; set;  }

        [Required]
        public int FirstNodeIndex { get; set; }

        [Required]
        public int FirstNodeCharIndex { get; set; }

        [Required]
        public int LastNodeIndex { get; set; }

        [Required]
        public int LastNodeCharIndex { get; set; }


        public int BookAnalysisId { get; set; }
        public virtual BookAnalysis BookAnalysis { get; set; }

        public virtual List<Tag> Tags { get; set; }
        //public virtual List<Note> Notes { get; set; }
    }
}
