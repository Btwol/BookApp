namespace BookApp.Server.Services.Interfaces.MapperServices
{
    public interface IBookAnalysisMapperService : IMapperService<BookAnalysis, BookAnalysisSummaryModel>
    {
        public void MapEditBookAnalysis(BookAnalysis analysistoUpdate, BookAnalysisSummaryModel updatedBookAnalysisModel);
        public Task<BookAnalysisDetailedModel> MapToDetailedModel(BookAnalysis bookAnalysis);
    }
}
