using BookApp.Shared.Interfaces.Model;
using BookApp.Shared.Models.ClientModels.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Shared.Models.ClientModels
{
    public class UpdatedBookAnalysisModel : IClientModel
    {
        public bool UpToDate { get; set; }
        public AnalysisVersionModel AnalysisVersion { get; set; }
        public BookAnalysisSummaryModel? BookAnalysisSummary { get; set; } = null;
        public List<TagModel>? Tags { get; set; } = null;
        public List<AnalysisNoteModel>? AnalysisNotes { get; set; } = null;
        public List<ChapterNoteModel>? ChapterNotes { get; set; } = null;
        public List<ParagraphNoteModel>? ParagraphNotes { get; set; } = null;
        public List<HighlightNoteModel>? HighlightNotes { get; set; } = null;
        public List<HighlightModel>? Highlights { get; set; } = null;
    }
}
