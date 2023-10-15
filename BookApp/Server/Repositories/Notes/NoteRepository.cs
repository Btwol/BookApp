using BookApp.Server.Repositories.Interfaces.Notes;

namespace BookApp.Server.Repositories.Notes
{
    public class NoteRepository : BaseRepository<INoteDBModel>, INoteRepository
    {
        public NoteRepository(DataContext context) : base(context)
        {
        }

        public override IQueryable<INoteDBModel> QueryWithIncludes(DbSet<INoteDBModel> querry)
        {
            return querry.Include(n => n.Tags);
        }
    }
}
