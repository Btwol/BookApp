using BookApp.Shared.Interfaces.Model;

namespace BookApp.Shared.Models.ClientModels.Notes
{
    public abstract class NoteModel : INoteModel
    {
        public int Id { get; set; }
        public int BookAnalysisId { get; set; }
        public string Content { get; set; }
    }
}
