namespace BookApp.Server.Services
{
    public class TagManagerServerService<T> : ITagManagerServerService<T> where T : ITaggable
    {
        private readonly ITagRepository _tagRepository;
        private readonly IBaseRepository<T> _taggedRepository;
        private readonly IBookAnalysisServerService _bookAnalysisServerService;

        public TagManagerServerService(IBaseRepository<T> taggedRepository, ITagRepository tagRepository, IBookAnalysisServerService bookAnalysisServerService)
        {
            _taggedRepository = taggedRepository;
            _tagRepository = tagRepository;
            _bookAnalysisServerService = bookAnalysisServerService;
        }

        public async Task<ServiceResponse> AddTag(int taggedItemId, int tagId)
        {
            var taggedItem = await _taggedRepository.FindByConditionsFirstOrDefault(h => h.Id == taggedItemId);
            var tag = await _tagRepository.FindByConditionsFirstOrDefault(t => t.Id == tagId);

            var validationResult = await ValidateTagRequest(taggedItem, tag);
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

        public async Task<ServiceResponse> RemoveTag(int taggedItemId, int tagId)
        {
            var taggedItem = await _taggedRepository.FindByConditionsFirstOrDefault(h => h.Id == taggedItemId);
            var tag = await _tagRepository.FindByConditionsFirstOrDefault(t => t.Id == tagId);

            var validationResult = await ValidateTagRequest(taggedItem, tag);
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

        private async Task<ServiceResponse> ValidateTagRequest(T? taggedItem, Tag? tag)
        {
            if (taggedItem is null)
            {
                ServiceResponse.Error("Item not found.");
            }

            if (tag is null)
            {
                ServiceResponse.Error("Tag not found.");
            }

            if (!await _bookAnalysisServerService.CurrentUserIsMemberTypeOfAnalysis(tag.BookAnalysisId, MemberType.Editor,
                MemberType.Moderator, MemberType.Administrator))
            {
                return ServiceResponse.Error("User needs to be at least an editor to modify tags.");
            }

            return ServiceResponse.Success();
        }
    }
}
