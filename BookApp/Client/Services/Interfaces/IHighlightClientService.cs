using BookApp.Shared.Data;
using BookApp.Shared.Interfaces.Services;

namespace BookApp.Client.Services.Interfaces
{
    public interface IHighlightClientService
    {
        public Task<ServiceResponse> UpdateHighlight(HighlightModel updatedHighlight);
        public Task<HttpResponseMessage> AddHighlight(HighlightModel newHighlight);
    }
}
