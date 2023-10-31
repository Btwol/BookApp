namespace BookApp.Server.Services.MapperServices
{
    public class BookAnalysisMapper : MapperService<BookAnalysis, BookAnalysisModel>, IBookAnalysisMapperService
    {
        public BookAnalysisMapper(IMapper mapper) : base(mapper)
        {
        }

        public void MapEditBookAnalysis(BookAnalysis analysistoUpdate, BookAnalysisModel updatedBookAnalysisModel)
        {
            analysistoUpdate.AnalysisTitle = updatedBookAnalysisModel.AnalysisTitle;
        }
    }
}
