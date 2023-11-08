using BookApp.Shared.Models.ClientModels;

namespace BookApp.Client.Services.Interfaces
{
    public interface IBookAnalysisStorage
    {
        public Task<BookAnalysisDetailedModel> GetLoadedBookAnalysis();
        public Task SetBookAnalysis(BookAnalysisDetailedModel bookAnalysis);
        public Task<bool> AnalysisIsLoaded();
    }
}
