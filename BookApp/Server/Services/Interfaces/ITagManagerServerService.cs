namespace BookApp.Server.Services.Interfaces
{
    public interface ITagManagerServerService<T> where T : ITaggable
    {
        public Task<ServiceResponse> AddTag(int taggedId, int tagId);
        public Task<ServiceResponse> RemoveTag(int taggedId, int tagId);
    }
}
