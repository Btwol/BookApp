namespace BookApp.Server.Repositories.Interfaces
{
    public interface ITaggableRepository<T> : IBaseRepository<T> where T : ITaggable
    {
    }
}
