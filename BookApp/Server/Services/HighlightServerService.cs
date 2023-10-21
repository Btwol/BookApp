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
            try
            {
                var mappedHighlight = _highlightMapperService.MapToDbModel(newHighlight);
                var addedHighlight = await _highlightRepository.Create(mappedHighlight);
                var mappedHighlightModel = _highlightMapperService.MapToClientModel(addedHighlight);
                return ServiceResponse<HighlightModel>.Success(mappedHighlightModel, "Highlight created.");
            }
            catch (Exception ex)
            {
                return ServiceResponse<Exception>.Error(ex, "Create highlight failed. " + ex.Message);
            }
        }

        public async Task<ServiceResponse> DeleteHighlight(int highlightId)
        {
            var highlightToDelete = await _highlightRepository.FindByConditionsFirstOrDefault(h => h.Id == highlightId);
            await _highlightRepository.Delete(highlightToDelete);

            return ServiceResponse.Success("Highlight deleted.");
        }

        public async Task<ServiceResponse> RetrieveHighlights(int bookAnalysisId)
        {
            try
            {
                var highlights = await _highlightRepository.FindByConditions(h => h.BookAnalysisId == bookAnalysisId);
                List<HighlightModel> mappedHighlights = new();
                foreach (Highlight highlight in highlights)
                {
                    mappedHighlights.Add(_highlightMapperService.MapToClientModel(highlight));
                }

                return ServiceResponse<List<HighlightModel>>.Success(mappedHighlights, "Highlights retrieved.");
            }
            catch (Exception ex)
            {
                return ServiceResponse<Exception>.Error(ex, "Retrieve highlights failed.");
            }
        }

        public async Task<ServiceResponse> UpdateHighlight(HighlightModel updatedHighlight)
        {
            try
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
            catch (Exception ex)
            {
                return ServiceResponse<Exception>.Error(ex, "Update highlight failed.");
            }
        }
    }
}
