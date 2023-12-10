using BookApp.Shared.Enums;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace BookApp.Server.Services.Notes
{
    public abstract class NoteServerService<D, C> : INoteServerService<D, C> where D : INoteDBModel where C : INoteClientModel
    {
        protected readonly INoteRepository<D> _noteRepository;
        protected readonly IBookAnalysisRepository _bookAnalysisRepository;
        protected readonly IBookAnalysisServerService _bookAnalysisServerService;
        protected readonly INoteMapper<D, C> _noteMapper;
        private readonly ITagRepository _tagRepository;
        private readonly IHubServerService _hubServerService;

        protected NoteServerService(INoteMapper<D, C> noteMapper, IBookAnalysisRepository bookAnalysisRepository,
            INoteRepository<D> noteRepository, IBookAnalysisServerService bookAnalysisServerService, IHubServerService hubServerService, ITagRepository tagRepository)
        {
            _noteMapper = noteMapper;
            _bookAnalysisRepository = bookAnalysisRepository;
            _noteRepository = noteRepository;
            _bookAnalysisServerService = bookAnalysisServerService;
            _hubServerService = hubServerService;
            _tagRepository = tagRepository;
        }

        public async Task<ServiceResponse> AddNote(C noteModel, int bookAnalysisId)
        {
            var validationResult = await ValidateNoteRequest(bookAnalysisId, noteModel);
            if (!validationResult.SuccessStatus)
            {
                return validationResult;
            }

            var noteSaveStatus = await SaveNote(noteModel);
            if (noteSaveStatus.SuccessStatus)
            {
                await _hubServerService.NoteCreated(bookAnalysisId, noteSaveStatus.Content);
            }

            return noteSaveStatus;
        }

        public async Task<ServiceResponse> DeleteNote(int noteId, int bookAnalysisId)
        {
            var noteToDelete = await _noteRepository.FindByConditionsFirstOrDefault(n => n.Id == noteId);
            if (noteToDelete is null)
            {
                return ServiceResponse.Error("Note not found.");
            }

            var validationResult = await ValidateNoteRequest(bookAnalysisId, noteToDelete);
            if (!validationResult.SuccessStatus)
            {
                return validationResult;
            }
            await _noteRepository.Delete(noteToDelete);

            await _hubServerService.NoteDeleted(bookAnalysisId, noteId, noteToDelete.GetType().Name.Replace("Model", string.Empty));
            return ServiceResponse.Success("Note deleted.");
        }

        public async Task<ServiceResponse> EditNote(C noteModel, int bookAnalysisId)
        {
            var validationResult = await ValidateNoteRequest(bookAnalysisId, noteModel);
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
            noteModel = await _noteMapper.MapToClientModel(noteToEdit);

            await _hubServerService.NoteUpdated(bookAnalysisId, noteModel);
            return ServiceResponse<C>.Success(noteModel, "Note edited.");
        }

        protected virtual void ModifyNote(C sourceNote, D destinationNote)
        {
            destinationNote.Content = sourceNote.Content;
        }

        protected virtual async Task<ServiceResponse> ValidateNoteRequest<T>(int bookAnalysisId, T noteModel) where T : INote
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

        protected virtual async Task<ServiceResponse<C>> SaveNote(C noteModel)
        {
            var mappedNote = await _noteMapper.MapToDbModel(noteModel);
            await IncludeTags(mappedNote);
            var savedNote = await _noteRepository.Create(mappedNote);
            var createdNote = await _noteMapper.MapToClientModel(savedNote);

            return ServiceResponse<C>.Success(createdNote, "Note added.");
        }

        protected virtual async Task IncludeTags(D mappedNote)
        {
            if (mappedNote.Tags.Count() > 0)
            {
                var tagsToAdd = new List<Tag>();
                foreach (var tag in mappedNote.Tags)
                {
                    var tagToAdd = await _tagRepository.FindByConditionsFirstOrDefault(t => t.Id == tag.Id);
                    if (tagToAdd is not null)
                    {
                        tagsToAdd.Add(tagToAdd);
                    }
                }
                mappedNote.Tags = tagsToAdd;
            }
        }
    }
}
