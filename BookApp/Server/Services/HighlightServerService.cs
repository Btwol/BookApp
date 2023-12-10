namespace BookApp.Server.Services
{
    public class HighlightServerService : IHighlightServerService
    {
        private readonly IHighlightRepository _highlightRepository;
        private readonly IHighlightMapper _highlightMapperService;
        private readonly IHubServerService _hubServerService;
        private readonly IBookAnalysisServerService _bookAnalysisServerService;

        public HighlightServerService(IHighlightRepository highlightRepository, IHighlightMapper highlightMapperService, 
            IHubServerService hubServerService, IBookAnalysisServerService bookAnalysisServerService)
        {
            _highlightRepository = highlightRepository;
            _highlightMapperService = highlightMapperService;
            _hubServerService = hubServerService;
            _bookAnalysisServerService = bookAnalysisServerService;
        }

        public async Task<ServiceResponse> AddHighlight(HighlightModel newHighlight)
        {
            var requestValidation = await ValidateHighlightRequest(newHighlight.BookAnalysisId);
            if (!requestValidation.SuccessStatus)
            {
                return ServiceResponse.Error(requestValidation.Message, requestValidation.StatusCode);
            }

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

            var requestValidation = await ValidateHighlightRequest(highlightToDelete.BookAnalysisId);
            if (!requestValidation.SuccessStatus)
            {
                return ServiceResponse.Error(requestValidation.Message, requestValidation.StatusCode);
            }

            await _highlightRepository.Delete(highlightToDelete);

            await _hubServerService.HighlightRemoved(highlightToDelete.BookAnalysisId, highlightId);
            return ServiceResponse.Success("Highlight deleted.");
        }

        public async Task<ServiceResponse> UpdateHighlight(HighlightModel updatedHighlight)
        {
            var requestValidation = await ValidateHighlightRequest(updatedHighlight.BookAnalysisId);
            if(!requestValidation.SuccessStatus)
            {
                return ServiceResponse.Error(requestValidation.Message, requestValidation.StatusCode);
            }

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

        private async Task<ServiceResponse> ValidateHighlightRequest(int bookAnalysisId)
        {
            if (!await _bookAnalysisServerService.CurrentUserIsMemberTypeOfAnalysis(bookAnalysisId, MemberType.Editor,
                MemberType.Moderator, MemberType.Administrator))
            {
                return ServiceResponse.Error("User needs to be at least an editor to modify highlights.", HttpStatusCode.Unauthorized);
            }

            return ServiceResponse.Success();
        }
    }
}
