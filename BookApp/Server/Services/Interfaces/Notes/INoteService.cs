namespace BookApp.Server.Services.Interfaces.Notes
{
    public interface INoteService<D, C> where D : INote where C : INoteModel
    {
        public Task<ServiceResponse> AddNote(C noteModel);
    }
}
