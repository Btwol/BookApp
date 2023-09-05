namespace BookApp.Server.Services
{
    public class TagServerService : ITagServerService
    {
        private readonly ITagRepository _tagRepository;
        private readonly ITagMapperService _tagMapperService;

        public TagServerService(ITagRepository tagRepository, ITagMapperService tagMapperService)
        {
            _tagRepository = tagRepository;
            _tagMapperService = tagMapperService;
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
