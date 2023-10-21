namespace BookApp.Server.Services.MapperServices
{
    public class BookAnalysisMapperService : MapperService<BookAnalysis, BookAnalysisModel>, IBookAnalysisMapperService
    {
        public BookAnalysisMapperService(IMapper mapper) : base(mapper)
        {
        }

        public void MapEditBookAnalysis(BookAnalysis analysistoUpdate, BookAnalysisModel updatedBookAnalysisModel)
        {
            analysistoUpdate.AnalysisTitle = updatedBookAnalysisModel.AnalysisTitle;
        }
    }
}
