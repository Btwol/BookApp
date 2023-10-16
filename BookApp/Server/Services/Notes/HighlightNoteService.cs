using BookApp.Server.Repositories.Interfaces.Notes;

namespace BookApp.Server.Services.Notes
{
    public class HighlightNoteService : NoteService<HighlightNote, HighlightNoteModel>, IHighlightNoteService
    {
        private readonly IHighlightRepository _highlightRepository;

        public HighlightNoteService(IHighlightNoteMapperService highlightNoteMapperService, IBookAnalysisRepository bookAnalysisRepository,
            IHighlightNoteRepository noteRepository, IBookAnalysisServerService bookAnalysisServerService, IHighlightRepository highlightRepository)
            : base(highlightNoteMapperService, bookAnalysisRepository, noteRepository, bookAnalysisServerService)
        {
            _highlightRepository = highlightRepository;
        }


        protected async override Task<ServiceResponse> ValidateNoteRequest<T>(int bookAnalysisId, T noteModel)
        {
            if(noteModel is HighlightNoteModel highlightNoteModel)
            {
                if (!await _highlightRepository.CheckIfExists(h => h.Id == highlightNoteModel.HighlightId))
                {
                    return ServiceResponse.Error("Highlight does not exist.");
                }
            }
            if (noteModel is HighlightNote highlightNote)
            {
                if (!await _highlightRepository.CheckIfExists(h => h.Id == highlightNote.HighlightId))
                {
                    return ServiceResponse.Error("Highlight does not exist.");
                }
            }
            return await base.ValidateNoteRequest(bookAnalysisId, noteModel);
        }
    }
}
