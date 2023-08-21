using BookApp.Server.Models;
using BookApp.Server.Repositories.Interfaces;
using BookApp.Server.Services.Interfaces;
using BookApp.Server.Services.Interfaces.MapperServices;
using BookApp.Shared.Data;

namespace BookApp.Server.Services
{
    public class HighlightService : IHighlightServerService
    {
        private readonly IHighlightRepository _highlightRepository;
        private readonly IHighlightMapperService _highlightMapperService;

        public HighlightService(IHighlightRepository highlightRepository, IHighlightMapperService highlightMapperService)
        {
            _highlightRepository = highlightRepository;
            _highlightMapperService = highlightMapperService;
        }

        public async Task<ServiceResponse> RetrieveHighlights(int bookAnalysisId)
        {
            try
            {
                var highlights = await _highlightRepository.FindByConditions(h => h.BookAnalysisId == bookAnalysisId);
                List<HighlightModel> mappedHighlights = new();
                foreach (Highlight highlight in highlights)
                {
                    mappedHighlights.Add(_highlightMapperService.MapToHighlightModel(highlight));
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
