using BookApp.Shared.Models.ClientModels.Notes;

namespace BookApp.Client.Services.Interfaces.Notes
{
    public interface INoteClientService<T> : ITagManagerClientService where T : INoteClientModel
    {
        public Task<T> AddNote(T noteModel);
        public Task<T> EditNote(T noteModel);
        public Task DeleteNote(int noteId, int bookAnalysisId);
    }
}
