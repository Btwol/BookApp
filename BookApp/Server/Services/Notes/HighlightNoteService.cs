using BookApp.Server.Repositories.Interfaces.Notes;

namespace BookApp.Server.Services.Notes
{
    public class HighlightNoteService : NoteService<HighlightNote, HighlightNoteModel>, IHighlightNoteService
    {
        private readonly IHighlightRepository _highlightRepository;

        public HighlightNoteService(IHighlightNoteMapperService highlightNoteMapperService, IBookAnalysisRepository bookAnalysisRepository,
            INoteRepository noteRepository, IBookAnalysisServerService bookAnalysisServerService, IHighlightRepository highlightRepository)
            : base(highlightNoteMapperService, bookAnalysisRepository, noteRepository, bookAnalysisServerService)
        {
            _highlightRepository = highlightRepository;
        }

        protected override async Task<ServiceResponse> ValidateNoteRequest(HighlightNoteModel noteModel)
        {
            if (!await _highlightRepository.CheckIfExists(h => h.Id == noteModel.HighlightId))
            {
                return ServiceResponse.Error("Highlight does not exist.");
            }

            return await base.ValidateNoteRequest(noteModel);
        }
    }
}
