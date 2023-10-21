namespace BookApp.Server.Repositories.Interfaces.Notes
{
    public interface INoteRepository<T> : IBaseRepository<T> where T : INoteDBModel
    {
    }
}
