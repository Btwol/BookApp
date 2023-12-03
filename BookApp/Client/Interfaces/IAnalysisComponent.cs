using BookApp.Shared.Models.ClientModels;

namespace BookApp.Client.Interfaces
{
    public interface IAnalysisComponent
    {
        public BookAnalysisDetailedModel BookAnalysis { get; set; }
        public Task ReRender();
    }
}
