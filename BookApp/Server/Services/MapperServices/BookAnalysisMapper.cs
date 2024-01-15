namespace BookApp.Server.Services.MapperServices
{
    public class BookAnalysisMapper : MapperService<BookAnalysis, BookAnalysisSummaryModel>, IBookAnalysisMapperService
    {
        private readonly IBookAnalysisUserRepository _bookAnalysisUserRepository;
        private readonly IAppUserMapperService _appUserMapperService;
        private readonly ITagMapper _tagMapper;

        public BookAnalysisMapper(IMapper mapper, IBookAnalysisUserRepository bookAnalysisUserRepository, IAppUserMapperService appUserMapperService, ITagMapper tagMapper) : base(mapper)
        {
            _bookAnalysisUserRepository = bookAnalysisUserRepository;
            _appUserMapperService = appUserMapperService;
            _tagMapper = tagMapper;
        }

        public void MapEditBookAnalysis(BookAnalysis analysistoUpdate, BookAnalysisSummaryModel updatedBookAnalysisModel)
        {
            analysistoUpdate.AnalysisTitle = updatedBookAnalysisModel.AnalysisTitle;
        }

        public async Task<BookAnalysisDetailedModel> MapToDetailedModel(BookAnalysis bookAnalysis)
        {
            var mappedAnalysis = _mapper.Map<BookAnalysisDetailedModel>(bookAnalysis);
            await IncludeMembers(bookAnalysis, mappedAnalysis);
            foreach (var tag in bookAnalysis.Tags)
            {
                foreach (var analysisNote in tag.AnalysisNotes)
                {
                    mappedAnalysis.AnalysisNotes.FirstOrDefault(a => a.Id == analysisNote.Id).Tags.Add(await _tagMapper.MapToClientModel(tag));
                }

                foreach (var chapterNote in tag.ChapterNotes)
                {
                    mappedAnalysis.ChapterNotes.FirstOrDefault(a => a.Id == chapterNote.Id).Tags.Add(await _tagMapper.MapToClientModel(tag));
                }

                foreach (var paragraphNote in tag.ParagraphNotes)
                {
                    mappedAnalysis.ParagraphNotes.FirstOrDefault(a => a.Id == paragraphNote.Id).Tags.Add(await _tagMapper.MapToClientModel(tag));
                }

                foreach (var highlight in tag.Highlights)
                {
                    mappedAnalysis.Highlights.FirstOrDefault(a => a.Id == highlight.Id).Tags.Add(await _tagMapper.MapToClientModel(tag));

                    foreach (var highlightNote in tag.HighlightNotes)
                    {
                        mappedAnalysis.Highlights.FirstOrDefault(h => h.Id == highlight.Id)
                            .HighlightNotes.FirstOrDefault(a => a.Id == highlightNote.Id).Tags.Add(await _tagMapper.MapToClientModel(tag));
                    }
                }
            }
            return mappedAnalysis;
        }

        public override async Task<BookAnalysisSummaryModel> MapToClientModel(BookAnalysis dbModel)
        {
            var mappedAnalysis = await base.MapToClientModel(dbModel);
            await IncludeMembers(dbModel, mappedAnalysis);
            return mappedAnalysis;
        }

        private async Task IncludeMembers(BookAnalysis bookAnalysis, BookAnalysisSummaryModel mapped)
        {
            foreach (var member in bookAnalysis.Users)
            {
                mapped.Members.Add(new KeyValuePair<Shared.Models.Identity.AppUserModel, MemberType>
                (
                    await _appUserMapperService.MapGetApiUserResponseDto(member),
                    (await _bookAnalysisUserRepository.FindByConditionsFirstOrDefault(au => au.UsersId == member.Id && au.BookAnalysisId == bookAnalysis.Id)).MemberType)
                );
            }
        }
    }
}
