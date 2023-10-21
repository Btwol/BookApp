namespace BookApp.Server.Models.Notes
{
    public class ParagraphNote : Note
    {
        public int TextNodeNumber { get; set; }
        public int PageNumber { get; set; }
        public int BookAnalysisId { get; set; }
        public virtual BookAnalysis BookAnalysis { get; set; }
    }
}
