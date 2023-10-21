namespace BookApp.Server.Repositories
{
    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(DataContext context) : base(context)
        {
        }


        public override IQueryable<Tag> QueryWithIncludes(DbSet<Tag> querry)
        {
            return querry
                .Include(t => t.AnalysisNotes)
                .Include(t => t.ParagraphNotes)
                .Include(t => t.ChapterNotes)
                .Include(t => t.Highlights);
        }
    }
}
