using BookApp.Shared.Models.ClientModels.Notes;

namespace BookApp.Client.Services.Interfaces.Notes
{
    public interface INoteClientService<T> : ITagManagerClientService where T : INoteClientModel 
    {
        public Task<HttpResponseMessage> AddNote(T noteModel);
        public Task<HttpResponseMessage> EditNote(T noteModel);
        public Task<HttpResponseMessage> DeleteNote(int noteId);
    }
}
