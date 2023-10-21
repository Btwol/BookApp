namespace BookApp.Client.Services.Interfaces
{
    public interface ITagManagerClientService
    {
        public Task<HttpResponseMessage> AddTag(int taggedItemId, int tagId);
        public Task<HttpResponseMessage> RemoveTag(int taggedItemId, int tagId);
    }
}
