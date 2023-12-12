namespace BookApp.Server.Models
{
    public class AnalysisVersion : IDbModel
    {
        public int Id { get; set; }
        public int AnalysisSummaryVersion { get; set; } = 1;    //title, members, other analysis metadata
        public int TagsVersion { get; set; } = 1;   //tags created/edited/deleted
        public int AnalysisNotesVersion { get; set; } = 1;  //notes added/deleted/edited
        public int ChapterNotesVersion { get; set; } = 1;
        public int ParagraphNotesVersion { get; set; } = 1;
        public int HighlightNotesVersion { get; set; } = 1;
        public int HighlightVersion { get; set; } = 1;

        public virtual BookAnalysis BookAnalysis { get; set; }
        //public int BookAnalysisId { get; set; }
    }
}
