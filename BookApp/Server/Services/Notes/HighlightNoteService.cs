namespace BookApp.Server.Services.Notes
{
    public class HighlightNoteService : NoteService<HighlightNote, HighlightNoteModel>, IHighlightNoteService
    {
        private readonly IHighlightRepository _highlightRepository;

        public HighlightNoteService(IHighlightNoteMapperService noteMapper, IBookAnalysisRepository bookAnalysisRepository, 
            IBaseRepository<HighlightNote> noteRepository, IHighlightRepository highlightRepository) 
            : base(noteMapper, bookAnalysisRepository, noteRepository)
        {
            _highlightRepository = highlightRepository;
        }

        protected override async Task<ServiceResponse> ValidateNote(HighlightNoteModel noteModel)
        {
            if (!await _highlightRepository.CheckIfExists(h => h.Id == noteModel.HighlightId))
            {
                return ServiceResponse.Error("Highlight does not exist.");
            }

            return await base.ValidateNote(noteModel);
        }
    }
}
