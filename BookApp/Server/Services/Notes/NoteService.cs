namespace BookApp.Server.Services.Notes
{
    public abstract class NoteService<D, C> where D : INote where C : INoteModel
    {
        private readonly IBaseRepository<D> _noteRepository;
        private readonly IBookAnalysisRepository _bookAnalysisRepository;
        private readonly INoteMapperService<D, C> _noteMapper;

        protected NoteService(INoteMapperService<D, C> noteMapper, IBookAnalysisRepository bookAnalysisRepository, 
            IBaseRepository<D> noteRepository)
        {
            _noteMapper = noteMapper;
            _bookAnalysisRepository = bookAnalysisRepository;
            _noteRepository = noteRepository;
        }

        public async Task<ServiceResponse> AddNote(C noteModel)
        {
            var validationResult = await ValidateNote(noteModel);
            if(!validationResult.SuccessStatus)
            {
                return validationResult;
            }

            return await SaveNote(noteModel);
        }

        protected virtual async Task<ServiceResponse> ValidateNote(C noteModel)
        {
            if (!await _bookAnalysisRepository.CheckIfExists(a => a.Id == noteModel.BookAnalysisId))
            {
                return ServiceResponse.Error("Book analysis does not exist.");
            }

            return ServiceResponse.Success();
        }

        protected virtual async Task<ServiceResponse> SaveNote(C noteModel)
        {
            var mappedNote = _noteMapper.MapToDbModel(noteModel);
            var savedNote = await _noteRepository.Create(mappedNote);
            var createdNote = _noteMapper.MapToClientModel(savedNote);

            return ServiceResponse<C>.Success(createdNote, "Note added.");
        }
    }
}
