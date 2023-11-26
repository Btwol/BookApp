using BookApp.Shared.Enums;

namespace BookApp.Server.Services
{
    public class TagServerService : ITagServerService
    {
        private readonly ITagRepository _tagRepository;
        private readonly ITagMapper _tagMapperService;
        private readonly IBookAnalysisServerService _bookAnalysisServerService;
        private readonly IHubServerService _hubServerService;

        public TagServerService(IBookAnalysisServerService bookAnalysisServerService, ITagMapper tagMapperService,
            ITagRepository tagRepository, IHubServerService hubServerService)
        {
            _bookAnalysisServerService = bookAnalysisServerService;
            _tagMapperService = tagMapperService;
            _tagRepository = tagRepository;
            _hubServerService = hubServerService;
        }

        public async Task<ServiceResponse> CreateNewTag(TagModel newTag, int bookAnalysisId)
        {
            var validationResult = await ValidateTagRequest(bookAnalysisId);
            if (!validationResult.SuccessStatus)
            {
                return validationResult;
            }

            var mappedTag = await _tagMapperService.MapToDbModel(newTag);
            mappedTag.BookAnalysisId = bookAnalysisId;

            var addedTag = await _tagRepository.Create(mappedTag);
            var mappedAddedTag = await _tagMapperService.MapToClientModel(addedTag);

            await _hubServerService.TagCreated(bookAnalysisId, mappedAddedTag);
            return ServiceResponse<TagModel>.Success(mappedAddedTag, "Tag created.");
        }

        public async Task<ServiceResponse> DeleteTag(int tagId)
        {
            var tagToRemove = await _tagRepository.FindByConditionsFirstOrDefault(t => t.Id == tagId);
            if (tagToRemove is null)
            {
                return ServiceResponse.Error("Tag does not exist.");
            }

            var validationResult = await ValidateTagRequest(tagToRemove.BookAnalysisId);
            if (!validationResult.SuccessStatus)
            {
                return validationResult;
            }

            await _tagRepository.DeleteById(tagId);

            await _hubServerService.TagDeleted(tagToRemove.BookAnalysisId, tagId);
            return ServiceResponse.Success("Tag deleted.");
        }

        public async Task<ServiceResponse> EditTag(TagModel editedTag)
        {
            var tagToEdit = await _tagRepository.FindByConditionsFirstOrDefault(t => t.Id == editedTag.Id);
            if (tagToEdit is null)
            {
                return ServiceResponse.Error("Tag does not exist.");
            }

            var validationResult = await ValidateTagRequest(tagToEdit.BookAnalysisId);
            if (!validationResult.SuccessStatus)
            {
                return validationResult;
            }

            tagToEdit.Name = editedTag.Name;

            await _tagRepository.Edit(tagToEdit);

            await _hubServerService.TagUpdated(tagToEdit.BookAnalysisId, editedTag);
            return ServiceResponse.Success("Tag edited.");
        }

        private async Task<ServiceResponse> ValidateTagRequest(int bookAnalysisId)
        {
            if (!await _bookAnalysisServerService.CurrentUserIsMemberTypeOfAnalysis(bookAnalysisId, MemberType.Editor,
                MemberType.Moderator, MemberType.Administrator))
            {
                return ServiceResponse.Error("User needs to be at least an editor to modify tags.");
            }

            return ServiceResponse.Success();
        }
    }
}
