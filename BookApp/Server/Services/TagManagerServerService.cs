namespace BookApp.Server.Services
{
    public class TagManagerServerService<T> : ITagManagerServerService<T> where T : ITaggable
    {
        private readonly ITagRepository _tagRepository;
        private readonly ITaggableRepository<T> _taggedRepository;
        private readonly IBookAnalysisServerService _bookAnalysisServerService;
        private readonly IHubServerService _hubServerService;

        public TagManagerServerService(ITaggableRepository<T> taggedRepository, ITagRepository tagRepository, IBookAnalysisServerService bookAnalysisServerService,
            IHubServerService hubServerService)
        {
            _taggedRepository = taggedRepository;
            _tagRepository = tagRepository;
            _bookAnalysisServerService = bookAnalysisServerService;
            _hubServerService = hubServerService;
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
                return ServiceResponse.Error("This item already has the tag.");
            }

            taggedItem.Tags.Add(tag);
            await _taggedRepository.Edit(taggedItem);

            await _hubServerService.TagAdded(tag.BookAnalysisId, tagId, taggedItemId, taggedItem.GetType().Name);
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
                return ServiceResponse.Error("This item doesn't have the tag.");
            }

            taggedItem.Tags.Remove(tag);
            await _taggedRepository.Edit(taggedItem);

            await _hubServerService.TagRemoved(tag.BookAnalysisId, tagId, taggedItemId, taggedItem.GetType().Name);
            return ServiceResponse.Success("Tag removed from item.");
        }

        private async Task<ServiceResponse> ValidateTagRequest(T? taggedItem, Tag? tag)
        {
            if (taggedItem is null)
            {
                return ServiceResponse.Error("Item not found.");
            }

            if (tag is null)
            {
                return ServiceResponse.Error("Tag not found.");
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
