using BookApp.Shared.Models.ClientModels;

namespace BookApp.Client.Services.Interfaces
{
    public interface IHighlightClientService : ITagManagerClientService
    {
        public Task UpdateHighlight(HighlightModel updatedHighlight);
        public Task<HighlightModel> AddHighlight(HighlightModel newHighlight);
        public Task DeleteHighlight(int highlightId);
    }
}
