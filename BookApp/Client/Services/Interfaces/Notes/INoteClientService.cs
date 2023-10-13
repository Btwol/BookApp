using BookApp.Shared.Models.ClientModels.Notes;

namespace BookApp.Client.Services.Interfaces.Notes
{
    public interface INoteClientService<T> where T : INoteClientModel
    {
        public Task<HttpResponseMessage> AddNote(T noteModel);
        public Task<HttpResponseMessage> EditNote(T noteModel);
        public Task<HttpResponseMessage> DeleteNote(int noteId);
        public Task<HttpResponseMessage> AddTag(int noteId, int tagId);
        public Task<HttpResponseMessage> RemoveTag(int noteId, int tagId);
    }
}
