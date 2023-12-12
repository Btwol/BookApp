using BookApp.Shared.Models.ClientModels.Notes;

namespace BookApp.Shared.Models.ClientModels
{
    public class BookAnalysisDetailedModel : BookAnalysisSummaryModel
    {
        public List<TagModel> Tags { get; set; } = new();
        public List<HighlightModel> Highlights { get; set; } = new();
        public List<AnalysisNoteModel> AnalysisNotes { get; set; } = new();
        public List<ChapterNoteModel> ChapterNotes { get; set; } = new();
        public List<ParagraphNoteModel> ParagraphNotes { get; set; } = new();
        public AnalysisVersionModel AnalysisVersion { get; set; } = new();

        public BookAnalysisDetailedModel()
        {

        }

        public BookAnalysisDetailedModel(BookAnalysisDetailedModel bookAnalysisModel)
        {
            this.Id = bookAnalysisModel.Id;
            this.AnalysisTitle = bookAnalysisModel.AnalysisTitle;
            this.BookHash = bookAnalysisModel.BookHash;
            this.BookTitle = bookAnalysisModel.BookHash;
            this.Authors = bookAnalysisModel.Authors;
            this.Tags = bookAnalysisModel.Tags;
            this.Highlights = bookAnalysisModel.Highlights;
            this.AnalysisNotes = bookAnalysisModel.AnalysisNotes;
            this.ChapterNotes = bookAnalysisModel.ChapterNotes;
            this.ParagraphNotes = bookAnalysisModel.ParagraphNotes;
        }
    }
}
