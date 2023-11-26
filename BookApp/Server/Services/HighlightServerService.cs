namespace BookApp.Server.Services
{
    public class HighlightServerService : IHighlightServerService
    {
        private readonly IHighlightRepository _highlightRepository;
        private readonly IHighlightMapper _highlightMapperService;
        private readonly IHubServerService _hubServerService;

        public HighlightServerService(IHighlightRepository highlightRepository, IHighlightMapper highlightMapperService, IHubServerService hubServerService)
        {
            _highlightRepository = highlightRepository;
            _highlightMapperService = highlightMapperService;
            _hubServerService = hubServerService;
        }

        public async Task<ServiceResponse> AddHighlight(HighlightModel newHighlight)
        {
            var mappedHighlight = await _highlightMapperService.MapToDbModel(newHighlight);
            var addedHighlight = await _highlightRepository.Create(mappedHighlight);
            var mappedHighlightModel = await _highlightMapperService.MapToClientModel(addedHighlight);

            await _hubServerService.HighlightAdded(mappedHighlightModel);
            return ServiceResponse<HighlightModel>.Success(mappedHighlightModel, "Highlight created.");
        }

        public async Task<ServiceResponse> DeleteHighlight(int highlightId)
        {
            var highlightToDelete = await _highlightRepository.FindByConditionsFirstOrDefault(h => h.Id == highlightId);
            if (highlightToDelete is null)
            {
                return ServiceResponse.Error("Highlight not found.");
            }

            await _highlightRepository.Delete(highlightToDelete);

            await _hubServerService.HighlightRemoved(highlightToDelete.BookAnalysisId, highlightId);
            return ServiceResponse.Success("Highlight deleted.");
        }

        public async Task<ServiceResponse> UpdateHighlight(HighlightModel updatedHighlight)
        {
            var highlightToUpdate = await _highlightRepository.FindByConditionsFirstOrDefault(h => h.Id == updatedHighlight.Id);
            if (highlightToUpdate is null)
            {
                return ServiceResponse.Error("Highlight not found");
            }

            _highlightMapperService.MapEditHighlight(highlightToUpdate, updatedHighlight);
            await _highlightRepository.Edit(highlightToUpdate);

            await _hubServerService.HighlightUpdated(updatedHighlight);
            return ServiceResponse.Success("Highlight updated.");
        }
    }
}
