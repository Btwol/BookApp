using BookApp.Shared.Interfaces.Model;
using System.ComponentModel.DataAnnotations;

namespace BookApp.Shared.Models.ClientModels.Notes
{
    public class NoteModel : INoteModel
    {
        public int Id { get; set; }
        public int BookAnalysisId { get; set; }

        [Required]
        [StringLength(2000, ErrorMessage = "Note is too long.")]
        public string Content { get; set; }
    }
}
