namespace BookApp.Client.Services.Interfaces
{
    public interface ITagManagerClientService
    {
        public Task AddTag(int taggedItemId, int tagId);
        public Task RemoveTag(int taggedItemId, int tagId);
    }
}
