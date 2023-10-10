using BookApp.Shared.Models.ClientModels.Notes;

namespace BookApp.Server.Services.Notes
{
    public abstract class NoteService<D, C> : INoteService<D, C> where D : INoteDBModel where C : INoteClientModel 
    {
        private readonly IBaseRepository<D> _noteRepository;
        private readonly IBookAnalysisRepository _bookAnalysisRepository;
        private readonly IBookAnalysisServerService _bookAnalysisServerService;
        private readonly INoteMapperService<D, C> _noteMapper;

        protected NoteService(INoteMapperService<D, C> noteMapper, IBookAnalysisRepository bookAnalysisRepository,
            IBaseRepository<D> noteRepository, IBookAnalysisServerService bookAnalysisServerService)
        {
            _noteMapper = noteMapper;
            _bookAnalysisRepository = bookAnalysisRepository;
            _noteRepository = noteRepository;
            _bookAnalysisServerService = bookAnalysisServerService;
        }

        public async Task<ServiceResponse> AddNote(C noteModel)
        {
            var validationResult = await ValidateNoteRequest(noteModel);
            if(!validationResult.SuccessStatus)
            {
                return validationResult;
            }

            return await SaveNote(noteModel);
        }

        public async Task<ServiceResponse> DeleteNote(int noteId)
        {
            var noteToDelete = await _noteRepository.FindByConditionsFirstOrDefault(n => n.Id == noteId);
            if(noteToDelete is null)
            {
                return ServiceResponse.Error("Note not found.");
            }

            var validationResult = await ValidateNoteRequest(noteId);
            if (!validationResult.SuccessStatus)
            {
                return validationResult;
            }

            await _noteRepository.Delete(noteToDelete);
            return ServiceResponse.Success("Note deleted.");
        }

        public async Task<ServiceResponse> EditNote(C noteModel)
        {
            var validationResult = await ValidateNoteRequest(noteModel);
            if (!validationResult.SuccessStatus)
            {
                return validationResult;
            }

            var noteToEdit = await _noteRepository.FindByConditionsFirstOrDefault(n => n.Id == noteModel.Id);
            if (noteToEdit is null)
            {
                return ServiceResponse.Error("Note not found.");
            }

            ModifyNote(noteModel, noteToEdit);

            await _noteRepository.Edit(noteToEdit);
            noteModel = _noteMapper.MapToClientModel(noteToEdit);
            return ServiceResponse<C>.Success(noteModel, "Note edited.");
        }

        protected virtual void ModifyNote(C sourceNote, D destinationNote)
        {
            destinationNote.Content = sourceNote.Content;
        }

        protected virtual async Task<ServiceResponse> ValidateNoteRequest(C noteModel)
        {
            if (!await _bookAnalysisRepository.CheckIfExists(a => a.Id == noteModel.BookAnalysisId))
            {
                return ServiceResponse.Error("Book analysis does not exist.");
            }

            if (!await _bookAnalysisServerService.CurrentUserIsMemberTypeOfAnalysis(noteModel.BookAnalysisId,
                MemberType.Administrator, MemberType.Moderator, MemberType.Editor))
            {
                return ServiceResponse.Error("Only the user with rights of editor or above can modify notes.", HttpStatusCode.Forbidden);
            }

            return ServiceResponse.Success();
        }

        protected virtual async Task<ServiceResponse> ValidateNoteRequest(int bookAnalysisId)
        {
            if (!await _bookAnalysisRepository.CheckIfExists(a => a.Id == bookAnalysisId))
            {
                return ServiceResponse.Error("Book analysis does not exist.");
            }

            if (!await _bookAnalysisServerService.CurrentUserIsMemberTypeOfAnalysis(bookAnalysisId,
                MemberType.Administrator, MemberType.Moderator, MemberType.Editor))
            {
                return ServiceResponse.Error("Only the user with rights of editor or above can modify notes.", HttpStatusCode.Forbidden);
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
