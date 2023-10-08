using BookApp.Shared.Models.ClientModels.Notes;

namespace BookApp.Client.Services.Interfaces.Notes
{
    public interface IAnalysisNoteClientService
    {
        public Task<HttpResponseMessage> AddAnalysisNote(AnalysisNoteModel analysisNoteModel);
    }
}
