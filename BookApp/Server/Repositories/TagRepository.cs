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
            foreach(var highlight in tagToDelete.Highlights)
            {
                highlight.Tags.Remove(tagToDelete);
            }

            foreach (var ChapterNotes in tagToDelete.ChapterNotes)
            {
                ChapterNotes.Tags.Remove(tagToDelete);
            }

            foreach (var AnalysisNotes in tagToDelete.AnalysisNotes)
            {
                AnalysisNotes.Tags.Remove(tagToDelete);
            }

            foreach (var ParagraphNotes in tagToDelete.ParagraphNotes)
            {
                ParagraphNotes.Tags.Remove(tagToDelete);
            }

            foreach (var HighlightNotes in tagToDelete.HighlightNotes)
            {
                HighlightNotes.Tags.Remove(tagToDelete);
            }

            _context.Remove(tagToDelete);
            await _context.SaveChangesAsync();
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
