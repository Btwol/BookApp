namespace BookApp.Server.Repositories
{
    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(DataContext context) : base(context)
        {
        }

        public async override Task DeleteById(int id)
        {
            var tagToDelete = await FindByConditionsFirstOrDefault(t => t.Id == id);

            tagToDelete.AnalysisNotes.Clear();
            tagToDelete.ChapterNotes.Clear();
            tagToDelete.ParagraphNotes.Clear();
            tagToDelete.HighlightNotes.Clear();
            tagToDelete.Highlights.Clear();

            _context.Remove(tagToDelete);
            await _context.SaveChangesAsync();
        }

        public override IQueryable<Tag> QueryWithIncludes(DbSet<Tag> querry)
        {
            return querry
                .Include(t => t.AnalysisNotes)
                .Include(t => t.ParagraphNotes)
                .Include(t => t.ChapterNotes)
                .Include(t => t.Highlights)
                .Include(t => t.HighlightNotes);
        }
    }
}
