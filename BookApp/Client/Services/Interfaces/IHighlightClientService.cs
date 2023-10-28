using BookApp.Shared.Models.ClientModels;
using BookApp.Shared.Models.Services;

namespace BookApp.Client.Services.Interfaces
{
    public interface IHighlightClientService : ITagManagerClientService
    {
        public Task UpdateHighlight(HighlightModel updatedHighlight);
        public Task<HighlightModel> AddHighlight(HighlightModel newHighlight);
        public Task DeleteHighlight(int highlightId);
    }
}
