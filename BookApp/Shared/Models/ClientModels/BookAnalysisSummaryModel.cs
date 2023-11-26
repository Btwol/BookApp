using BookApp.Shared.Enums;
using BookApp.Shared.Interfaces.Model;
using BookApp.Shared.Models.Identity;
using System.ComponentModel.DataAnnotations;

namespace BookApp.Shared.Models.ClientModels
{
    public class BookAnalysisSummaryModel : IClientModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Analysis title is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Analysis title must be between 3 and 30 characters.")]
        public string AnalysisTitle { get; set; }
        public string BookHash { get; set; }
        public string BookTitle { get; set; }
        public List<string> Authors { get; set; } = new();
        public List<KeyValuePair<AppUserModel, MemberType>> Members { get; set; } = new();

        public BookAnalysisSummaryModel()
        {

        }

        public BookAnalysisSummaryModel(BookAnalysisSummaryModel bookAnalysisSummary)
        {
            this.Id = bookAnalysisSummary.Id;
            this.AnalysisTitle = bookAnalysisSummary.AnalysisTitle;
            this.BookHash = bookAnalysisSummary.BookHash;
            this.BookTitle = bookAnalysisSummary.BookTitle;
            this.Authors = bookAnalysisSummary.Authors;
            this.Members = bookAnalysisSummary.Members;
        }
    }
}
