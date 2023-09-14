using BookApp.Shared.Models.ClientModels;

namespace BookApp.Server.Services.Interfaces.MapperServices
{
    public interface IBookAnalysisMapper
    {
        public BookAnalysis MapToBookAnalysis(BookAnalysisModel bookAnalysisModel);
        public BookAnalysisModel MapToBookAnalysisModel(BookAnalysis bookAnalysis);
        public void MapEditBookAnalysis(BookAnalysis analysistoUpdate, BookAnalysisModel updatedBookAnalysisModel);
    }
}
