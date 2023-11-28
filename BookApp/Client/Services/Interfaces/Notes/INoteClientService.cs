using BookApp.Shared.Models.ClientModels.Notes;

namespace BookApp.Client.Services.Interfaces.Notes
{
    public interface INoteClientService
    {
        public Task<INoteClientModel> AddNote(INoteClientModel noteModel);
        public Task<INoteClientModel> EditNote(INoteClientModel noteModel);
        public Task DeleteNote(int noteId, int bookAnalysisId);
    }
}
