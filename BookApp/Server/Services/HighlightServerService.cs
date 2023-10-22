namespace BookApp.Server.Services
{
    public class HighlightServerService : IHighlightServerService
    {
        private readonly IHighlightRepository _highlightRepository;
        private readonly IHighlightMapperService _highlightMapperService;

        public HighlightServerService(IHighlightRepository highlightRepository, IHighlightMapperService highlightMapperService)
        {
            _highlightRepository = highlightRepository;
            _highlightMapperService = highlightMapperService;
        }

        public async Task<ServiceResponse> AddHighlight(HighlightModel newHighlight)
        {
            var mappedHighlight = _highlightMapperService.MapToDbModel(newHighlight);
            var addedHighlight = await _highlightRepository.Create(mappedHighlight);
            var mappedHighlightModel = _highlightMapperService.MapToClientModel(addedHighlight);
            return ServiceResponse<HighlightModel>.Success(mappedHighlightModel, "Highlight created.");
        }

        public async Task<ServiceResponse> DeleteHighlight(int highlightId)
        {
            var highlightToDelete = await _highlightRepository.FindByConditionsFirstOrDefault(h => h.Id == highlightId);
            await _highlightRepository.Delete(highlightToDelete);

            return ServiceResponse.Success("Highlight deleted.");
        }

        public async Task<ServiceResponse> RetrieveHighlights(int bookAnalysisId)
        {
            var highlights = await _highlightRepository.FindByConditions(h => h.BookAnalysisId == bookAnalysisId);
            List<HighlightModel> mappedHighlights = new();
            foreach (Highlight highlight in highlights)
            {
                mappedHighlights.Add(_highlightMapperService.MapToClientModel(highlight));
            }

            return ServiceResponse<List<HighlightModel>>.Success(mappedHighlights, "Highlights retrieved.");
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

            return ServiceResponse.Success("Highlight updated.");
        }
    }
}
