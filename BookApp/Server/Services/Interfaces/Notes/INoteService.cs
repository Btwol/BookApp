namespace BookApp.Server.Services.Interfaces.Notes
{
    public interface INoteService<D, C> where D : INoteDBModel where C : INoteClientModel
    {
        public Task<ServiceResponse> AddNote(C noteModel);
        public Task<ServiceResponse> DeleteNote(int noteId);
        public Task<ServiceResponse> EditNote(C noteModel);
    }
}
