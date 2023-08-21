using BookApp.Client.Services.Interfaces;
using BookApp.Server.Database;
using BookApp.Server.Repositories.Interfaces;
using BookApp.Server.Services.Interfaces;
using BookApp.Server.Services.Interfaces.MapperServices;
using BookApp.Shared.Data;
using System.Net;

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
            try
            {




                throw new InvalidCastException();





                await _bookAnalysisRepository.Create(newAnalysis);
                return ServiceResponse.Success("Analysis created.");
            }
            catch (Exception ex)
            {
                return ServiceResponse<Exception>.Error(ex, "Create new analysis failed.");
            }
        }

        public async Task<ServiceResponse> GetAnalysisByHash(string bookHash)
        {
            try
            {
                var foundAnalyses = await _bookAnalysisRepository.FindByConditions(b => b.BookHash == bookHash);
                List<BookAnalysisModel> mappedAnalyses = new();
                foreach (var analysis in foundAnalyses)
                {
                    mappedAnalyses.Add(_bookAnalysisMapper.MapToBookAnalysisModel(analysis));
                }

                return ServiceResponse<List<BookAnalysisModel>>.Success(mappedAnalyses, "Analyses retrieved.");
            }
            catch (Exception ex)
            {
                return ServiceResponse<Exception>.Error(ex, "Create new analysis failed.");
            }
        }

        public async Task<ServiceResponse> GetBookAnalysis(int analysisId)
        {
            try
            {
                var analysis = await _bookAnalysisRepository.FindByConditionsFirstOrDefault(b => b.Id == analysisId);
                if (analysis is null)
                {
                    return ServiceResponse.Error("Analysis not found", HttpStatusCode.NotFound);
                }

                var mappedAnalysis = _bookAnalysisMapper.MapToBookAnalysisModel(analysis);
                return ServiceResponse<BookAnalysisModel>.Success(mappedAnalysis, "Analysis retrieved.");
            }
            catch (Exception ex)
            {
                return ServiceResponse<Exception>.Error(ex, "Create new analysis failed.");
            }
        }

        public async Task<ServiceResponse> UpdateBookAnalysis(BookAnalysisModel updatedBookAnalysisModel)
        {
            try
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
            catch (Exception ex)
            {
                return ServiceResponse<Exception>.Error(ex, "Create new analysis failed.");
            }
        }
    }
}
