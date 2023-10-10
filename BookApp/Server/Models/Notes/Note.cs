namespace BookApp.Server.Models.Notes
{
    public abstract class Note : INoteDBModel
    {
        public int Id { get; set; }
        public int BookAnalysisId { get; set; }
        public virtual BookAnalysis BookAnalysis { get; set; }
        public string Content { get; set; }
    }
}
