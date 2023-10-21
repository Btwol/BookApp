namespace BookApp.Server.Models.Notes
{
    public class AnalysisNote : Note
    {
        public int BookAnalysisId { get; set; }
        public virtual BookAnalysis BookAnalysis { get; set; }
    }
}
