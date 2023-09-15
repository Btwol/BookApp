using BookApp.Shared.Models.ClientModels;
using BookApp.Shared.Models.Services;

namespace BookApp.Server.Services
{
    public class BookAnalysisServerService : IBookAnalysisServerService
    {
        private readonly IBookAnalysisMapper _bookAnalysisMapper;
        private readonly IBookAnalysisRepository _bookAnalysisRepository;

        public BookAnalysisServerService(IBookAnalysisMapper bookAnalysisMapper,
            IBookAnalysisRepository bookAnalysisRepository)
        {
            _bookAnalysisMapper = bookAnalysisMapper;
            _bookAnalysisRepository = bookAnalysisRepository;
        }

        public async Task<ServiceResponse> CreateBookAnalysis(BookAnalysisModel newAnalysisModel)
        {
            var newAnalysis = _bookAnalysisMapper.MapToBookAnalysis(newAnalysisModel);
            newAnalysis = await _bookAnalysisRepository.Create(newAnalysis);

            newAnalysisModel = _bookAnalysisMapper.MapToBookAnalysisModel(newAnalysis);
            return ServiceResponse<BookAnalysisModel>.Success(newAnalysisModel, "Analysis created.");
        }

        public async Task<ServiceResponse> GetAnalysisByHash(string bookHash)
        {
            if (bookHash == "22836997B8F82310BA982D76BF1A6CED395FB1226E7BCC26DBF2FE72395E02C1")
                throw new Exception("Test exception thrown: GetAnalysisByHash server service");

            var foundAnalyses = await _bookAnalysisRepository.FindByConditions(b => b.BookHash == bookHash);
            List<BookAnalysisModel> mappedAnalyses = new();
            foreach (var analysis in foundAnalyses)
            {
                mappedAnalyses.Add(_bookAnalysisMapper.MapToBookAnalysisModel(analysis));
            }

            return ServiceResponse<List<BookAnalysisModel>>.Success(mappedAnalyses, "Analyses retrieved.");
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

        public async Task<ServiceResponse> UpdateBookAnalysis(BookAnalysisModel updatedBookAnalysisModel)
        {
            var analysistoUpdate = await _bookAnalysisRepository.FindByConditionsFirstOrDefault(a => a.Id == updatedBookAnalysisModel.Id);
            if (analysistoUpdate is null)
            {
                return ServiceResponse.Error("Analysis not found", HttpStatusCode.NotFound);
            }

            _bookAnalysisMapper.MapEditBookAnalysis(analysistoUpdate, updatedBookAnalysisModel);
            await _bookAnalysisRepository.Edit(analysistoUpdate);

            return ServiceResponse.Success("Analysis updated.");
        }
    }
}
