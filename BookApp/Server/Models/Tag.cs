namespace BookApp.Server.Models
{
    public class Tag : IDbModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int BookAnalysisId { get; set; }
        public virtual BookAnalysis BookAnalysis { get; set; }

        public virtual List<Highlight>? Highlights { get; set; }
        public virtual List<AnalysisNote>? AnalysisNotes { get; set; }
        public virtual List<ParagraphNote>? ParagraphNotes { get; set; }
        public virtual List<ChapterNote>? ChapterNotes { get; set; }
        public virtual List<HighlightNote>? HighlightNotes { get; set; } = new();
    }
}
