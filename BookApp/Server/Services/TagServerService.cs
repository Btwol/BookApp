
namespace BookApp.Server.Services
{
    public class TagServerService<T> : ITagServerService<T> where T : ITaggable
    {
        private readonly ITagRepository _tagRepository;
        private readonly ITagMapper _tagMapperService;
        private readonly IBaseRepository<T> _taggedRepository;
        private readonly IBookAnalysisServerService _bookAnalysisServerService;

        public TagServerService(IBaseRepository<T> taggedRepository, ITagMapper tagMapperService, ITagRepository tagRepository,
            IBookAnalysisServerService bookAnalysisServerService)
        {
            _taggedRepository = taggedRepository;
            _tagMapperService = tagMapperService;
            _tagRepository = tagRepository;
            _bookAnalysisServerService = bookAnalysisServerService;
        }

        public async Task<ServiceResponse> AddTag(int taggedItemId, int tagId)
        {
            var taggedItem = await _taggedRepository.FindByConditionsFirstOrDefault(h => h.Id == taggedItemId);
            var tag = await _tagRepository.FindByConditionsFirstOrDefault(t => t.Id == tagId);

            var validationResult = ValidateTagRequest(taggedItem, tag);
            if (!validationResult.SuccessStatus)
            {
                return validationResult;
            }

            if (taggedItem.Tags.Any(tag => tag.Id == tagId))
            {
                ServiceResponse.Error("This item already has an the tag.");
            }

            taggedItem.Tags.Add(tag);
            await _taggedRepository.Edit(taggedItem);

            return ServiceResponse.Success("Tag added to item.");
        }

        private static ServiceResponse ValidateTagRequest(T? taggedItem, Tag? tag)
        {
            if (taggedItem is null)
            {
                ServiceResponse.Error("Item not found.");
            }

            if (tag is null)
            {
                ServiceResponse.Error("Tag not found.");
            }

            return ServiceResponse.Success();
        }

        public async Task<ServiceResponse> RemoveTag(int taggedItemId, int tagId)
        {
            var taggedItem = await _taggedRepository.FindByConditionsFirstOrDefault(h => h.Id == taggedItemId);
            var tag = await _tagRepository.FindByConditionsFirstOrDefault(t => t.Id == tagId);

            var validationResult = ValidateTagRequest(taggedItem, tag);
            if (!validationResult.SuccessStatus)
            {
                return validationResult;
            }

            if (!taggedItem.Tags.Any(tag => tag.Id == tagId))
            {
                ServiceResponse.Error("This item doesn't have the tag.");
            }

            taggedItem.Tags.Remove(tag);
            await _taggedRepository.Edit(taggedItem);

            return ServiceResponse.Success("Tag removed from item.");
        }









        public async Task<ServiceResponse> GetTags(int bookAnalysisId)
        {
            var tags = await _tagRepository.FindByConditions(t => t.BookAnalysisId == bookAnalysisId);
            var mappedTags = new List<TagModel>();
            foreach (var tag in tags)
            {
                mappedTags.Add(_tagMapperService.MapToClientModel(tag));
            }

            return ServiceResponse<List<TagModel>>.Success(mappedTags, "Tags retrieved.");
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

            return ServiceResponse.Success("Tag deleted.");
        }

        public async Task<ServiceResponse> CreateNewTag(TagModel newTag, int bookAnalysisId)
        {
            var validationResult = await ValidateTagRequest(bookAnalysisId);
            if (!validationResult.SuccessStatus)
            {
                return validationResult;
            }

            var mappedTag = _tagMapperService.MapToDbModel(newTag);
            mappedTag.BookAnalysisId = bookAnalysisId;

            var addedTag = await _tagRepository.Create(mappedTag);
            var mappedAddedTag = _tagMapperService.MapToClientModel(addedTag);

            return ServiceResponse<TagModel>.Success(mappedAddedTag, "Tag created.");
        }

        private async Task<ServiceResponse> ValidateTagRequest(int bookAnalysisId)
        {
            if (!await _bookAnalysisServerService.CurrentUserIsMemberTypeOfAnalysis(bookAnalysisId, MemberType.Editor,
                MemberType.Moderator, MemberType.Administrator))
            {
                return ServiceResponse.Error("User needs to be at least an editor to delete a tag.");
            }

            return ServiceResponse.Success();
        }
    }
}
