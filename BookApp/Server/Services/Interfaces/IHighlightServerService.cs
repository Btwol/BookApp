using BookApp.Shared.Models.ClientModels;
using BookApp.Shared.Models.Services;

namespace BookApp.Server.Services.Interfaces
{
    public interface IHighlightServerService
    {
        public Task<ServiceResponse> AddHighlight(HighlightModel newHighlight);
        public Task<ServiceResponse> DeleteHighlight(int highlightId);
    }
}
