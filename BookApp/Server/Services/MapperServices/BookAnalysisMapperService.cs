namespace BookApp.Server.Services.MapperServices
{
    public class BookAnalysisMapperService : IBookAnalysisMapper
    {
        private readonly IMapper _mapper;

        public BookAnalysisMapperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void MapEditBookAnalysis(BookAnalysis analysistoUpdate, BookAnalysisModel updatedBookAnalysisModel)
        {
            analysistoUpdate.AnalysisTitle = updatedBookAnalysisModel.AnalysisTitle;
            analysistoUpdate.Authors = String.Join(", ", updatedBookAnalysisModel.Authors);
            analysistoUpdate.BookTitle = updatedBookAnalysisModel.BookTitle;
            analysistoUpdate.BookHash = updatedBookAnalysisModel.BookHash;
        }

        public BookAnalysis MapToBookAnalysis(BookAnalysisModel bookAnalysisModel)
        {
            return _mapper.Map<BookAnalysis>(bookAnalysisModel);
        }

        public BookAnalysisModel MapToBookAnalysisModel(BookAnalysis bookAnalysis)
        {
            return _mapper.Map<BookAnalysisModel>(bookAnalysis);
        }
    }
}
