using BookApp.Client.Interfaces;
using BookApp.Shared.Models.ClientModels;

namespace BookApp.Client.Services.Interfaces
{
    public interface IAnalysisUpdaterClientService
    {
        public Task UpdateAnalysisModel(IAnalysisComponent analysisComponent);
    }
}
