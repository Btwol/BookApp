namespace BookApp.Server.Services
{
    public class BookAnalysisServerService : IBookAnalysisServerService
    {
        private readonly IBookAnalysisMapper _bookAnalysisMapper;
        private readonly IBookAnalysisRepository _bookAnalysisRepository;
        private readonly IAppUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IBaseRepository<BookAnalysisUser> _bookAnalysisUserRepository;

        public BookAnalysisServerService(IBookAnalysisMapper bookAnalysisMapper,
            IBookAnalysisRepository bookAnalysisRepository,
            IAppUserService userService,
            UserManager<AppUser> userManager,
            IBaseRepository<BookAnalysisUser> bookAnalysisUserRepository)
        {
            _bookAnalysisMapper = bookAnalysisMapper;
            _bookAnalysisRepository = bookAnalysisRepository;
            _userService = userService;
            _userManager = userManager;
            _bookAnalysisUserRepository = bookAnalysisUserRepository;
        }

        public async Task<ServiceResponse> CreateBookAnalysis(BookAnalysisModel newAnalysisModel)
        {
            var newAnalysis = _bookAnalysisMapper.MapToBookAnalysis(newAnalysisModel);

            var creator = await _userManager.FindByIdAsync(_userService.GetCurrentUserId().ToString());
            newAnalysis.Users.Add(creator);

            newAnalysis = await _bookAnalysisRepository.Create(newAnalysis);
            newAnalysisModel = _bookAnalysisMapper.MapToBookAnalysisModel(newAnalysis);

            return ServiceResponse<BookAnalysisModel>.Success(newAnalysisModel, "Analysis created.");
        }

        public async Task<ServiceResponse> DeleteBookAnalysis(int analysisId)
        {
            var analysisToDelete = await _bookAnalysisRepository.FindByConditionsFirstOrDefault(ba => ba.Id == analysisId);
            if(analysisToDelete is null)
            {
                return ServiceResponse.Error("Analysis not found", HttpStatusCode.NotFound);   
            }

            if(!await CurrentUserIsMemberTypeOfAnalysis(analysisId, MemberType.Administrator))
            {
                return ServiceResponse.Error("Only the analysis administrator can delete it.", HttpStatusCode.Forbidden);
            }

            await _bookAnalysisRepository.Delete(analysisToDelete);
            return ServiceResponse.Success("Analysis deleted.");
        }

        public async Task<ServiceResponse> GetAnalysisByHash(string bookHash)
        {
            try
            {
                //int zero = 0;
                //int a = 10 / zero;

                var foundAnalyses = await _bookAnalysisRepository.FindByConditions(b => b.BookHash == bookHash);
                List<BookAnalysisModel> mappedAnalyses = new();
                foreach (var analysis in foundAnalyses)
                {
                    mappedAnalyses.Add(_bookAnalysisMapper.MapToBookAnalysisModel(analysis));
                }

                return ServiceResponse<List<BookAnalysisModel>>.Success(mappedAnalyses, "Analyses retrieved.");
            }
            catch
            {
                return ServiceResponse.Error("An error occured while attempting to get analyses by hash.");
            }
        }

        public async Task<ServiceResponse> GetBookAnalysis(int analysisId)
        {
            var analysis = await _bookAnalysisRepository.FindByConditionsFirstOrDefault(b => b.Id == analysisId);
            if (analysis is null)
            {
                return ServiceResponse.Error("Analysis not found", HttpStatusCode.NotFound);
            }

            var mappedAnalysis = _bookAnalysisMapper.MapToBookAnalysisModel(analysis);
            return ServiceResponse<BookAnalysisModel>.Success(mappedAnalysis, "Analysis retrieved.");
        }

        public async Task<ServiceResponse> EditBookAnalysis(BookAnalysisModel updatedBookAnalysisModel)
        {
            var analysistoUpdate = await _bookAnalysisRepository.FindByConditionsFirstOrDefault(a => a.Id == updatedBookAnalysisModel.Id);
            if (analysistoUpdate is null)
            {
                return ServiceResponse.Error("Analysis not found", HttpStatusCode.NotFound);
            }

            if (!await CurrentUserIsMemberTypeOfAnalysis(analysistoUpdate.Id, MemberType.Administrator, MemberType.Moderator))
            {
                return ServiceResponse.Error("Only the analysis moderator or greater can modify it.", HttpStatusCode.Forbidden);
            }

            _bookAnalysisMapper.MapEditBookAnalysis(analysistoUpdate, updatedBookAnalysisModel);
            await _bookAnalysisRepository.Edit(analysistoUpdate);

            return ServiceResponse.Success("Analysis updated.");
        }

        public async Task<bool> CurrentUserIsMemberTypeOfAnalysis(int analysisId, params MemberType[] memberType)
        {
            var memberTypes = memberType.ToList();
            return await _bookAnalysisUserRepository.CheckIfExists(bau =>
                bau.UsersId == _userService.GetCurrentUserId()
                && bau.BookAnalysisId == analysisId
                && memberTypes.Contains(bau.MemberType));
        }
    }
}
