namespace BookApp.Server.Models
{
    public class Highlight : ITaggable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Chapter { get; set; }

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

        public virtual List<Tag>? Tags { get; set; } = new();
        public virtual List<HighlightNote>? HighlightNotes { get; set; }
    }
}
