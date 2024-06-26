﻿namespace BookApp.Server.Repositories.Notes
{
    public class HighlightNoteRepository : NoteRepository<HighlightNote>, IHighlightNoteRepository
    {
        public HighlightNoteRepository(DataContext context) : base(context) { }

        public override IQueryable<HighlightNote> QueryWithIncludes(DbSet<HighlightNote> querry)
        {
            return querry
                .Include(n => n.Highlight).ThenInclude(h => h.BookAnalysis)
                .Include(n => n.Tags);
        }
    }
}
