namespace BookApp.Server.Services.Interfaces
{
    public interface IHighlightServerService
    {
        public Task<ServiceResponse> AddHighlight(HighlightModel newHighlight);
        public Task<ServiceResponse> DeleteHighlight(int highlightId);
        public Task<ServiceResponse> UpdateHighlight(HighlightModel updatedHighlight);
    }
}
