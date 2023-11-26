using BookApp.Server.Repositories.Interfaces.Notes;

namespace BookApp.Server.Repositories.Notes
{
    public class NoteRepository<T> : BaseRepository<T>, INoteRepository<T> where T : Note
    {
        public NoteRepository(DataContext context) : base(context)
        {
        }

        public override IQueryable<T> QueryWithIncludes(DbSet<T> querry)
        {
            return querry.Include(n => n.Tags);
        }

        public override Task Delete(T model)
        {
            model.Tags.Clear();
            return base.Delete(model);
        }
    }
}
