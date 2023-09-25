using BookApp.Shared.Models.ClientModels;
using BookApp.Shared.Models.Services;

namespace BookApp.Server.Services
{
    public class TagServerService : ITagServerService
    {
        private readonly ITagRepository _tagRepository;
        private readonly ITagMapperService _tagMapperService;
        private readonly IHighlightRepository _highlightRepository;

        public TagServerService(ITagRepository tagRepository, ITagMapperService tagMapperService, IHighlightRepository highlightRepository)
        {
            _tagRepository = tagRepository;
            _tagMapperService = tagMapperService;
            _highlightRepository = highlightRepository;
        }

        public async Task<ServiceResponse> AddTag(int highlightId, int tagId)
        {
            var highlight = await _highlightRepository.FindByConditionsFirstOrDefault(h => h.Id == highlightId);
            var tag = await _tagRepository.FindByConditionsFirstOrDefault(t => t.Id == tagId);

            highlight.Tags.Add(tag);
            await _highlightRepository.Edit(highlight);

            return ServiceResponse.Success("Tag added to highlight.");
        }

        public async Task<ServiceResponse> RemoveTag(int highlightId, int tagId)
        {
            var highlight = await _highlightRepository.FindByConditionsFirstOrDefault(h => h.Id == highlightId);
            var tag = await _tagRepository.FindByConditionsFirstOrDefault(t => t.Id == tagId);

            highlight.Tags.Remove(tag);
            await _highlightRepository.Edit(highlight);

            return ServiceResponse.Success("Tag removed from highlight.");
        }

        public async Task<ServiceResponse> CreateNewTag(TagModel newTag, int bookAnalysisId)
        {
            var mappedTag = _tagMapperService.MapToDbModel(newTag);
            mappedTag.BookAnalysisId = bookAnalysisId;

            var addedTag = await _tagRepository.Create(mappedTag);
            var mappedAddedTag = _tagMapperService.MapToClientModel(addedTag);

            return ServiceResponse<TagModel>.Success(mappedAddedTag, "Tag created.");
        }

        public async Task<ServiceResponse> GetTags(int bookAnalysisId)
        {
            var tags = await _tagRepository.FindByConditions(t => t.BookAnalysisId == bookAnalysisId);
            var mappedTags = new List<TagModel>();
            foreach(var tag in tags)
            {
                mappedTags.Add(_tagMapperService.MapToClientModel(tag));
            }

            return ServiceResponse<List<TagModel>>.Success(mappedTags, "Tags retrieved.");
        }
    }
}
