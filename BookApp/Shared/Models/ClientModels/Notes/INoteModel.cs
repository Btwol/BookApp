using BookApp.Shared.Interfaces.Model;

namespace BookApp.Shared.Models.ClientModels.Notes
{
    public interface INoteModel : IClientModel
    {
        public int BookAnalysisId { get; set; }
    }
}
