namespace BookApp.Server.Services.Interfaces.MapperServices
{
    public interface IBookAnalysisMapperService : IMapperService<BookAnalysis, BookAnalysisModel>
    {
        public void MapEditBookAnalysis(BookAnalysis analysistoUpdate, BookAnalysisModel updatedBookAnalysisModel);
    }
}
