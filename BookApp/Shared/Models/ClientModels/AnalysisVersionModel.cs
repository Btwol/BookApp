namespace BookApp.Shared.Models.ClientModels
{
    public class AnalysisVersionModel
    {
        public int AnalysisSummaryVersion { get; set; }
        public int TagsVersion { get; set; } 
        public int AnalysisNotesVersion { get; set; } 
        public int ChapterNotesVersion { get; set; }
        public int ParagraphNotesVersion { get; set; }
        public int HighlightNotesVersion { get; set; }
        public int HighlightVersion { get; set; }
        public int BookAnalysisId { get; set; }

        public override bool Equals(object? obj)
        {
            AnalysisVersionModel analysisVersion = obj as AnalysisVersionModel;
            return this.AnalysisNotesVersion == analysisVersion.AnalysisNotesVersion
                && this.AnalysisSummaryVersion == analysisVersion.AnalysisSummaryVersion
                && this.TagsVersion == analysisVersion.TagsVersion
                && this.ChapterNotesVersion == analysisVersion.ChapterNotesVersion
                && this.ParagraphNotesVersion == analysisVersion.ParagraphNotesVersion
                && this.HighlightNotesVersion == analysisVersion.HighlightNotesVersion
                && this.HighlightVersion == analysisVersion.HighlightVersion;
        }
    }
}
