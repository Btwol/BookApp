namespace BookApp.Server.Repositories
{
    public class BookAnalysisRepository : BaseRepository<BookAnalysis>, IBookAnalysisRepository
    {
        public BookAnalysisRepository(DataContext context) : base(context)
        {

        }

        public override async Task Delete(BookAnalysis bookAnalysisToDelete)
        {
            var tags = bookAnalysisToDelete.Tags;
            var analysisNotes = bookAnalysisToDelete.AnalysisNotes;
            var chapterNotes = bookAnalysisToDelete.ChapterNotes;
            var paragraphNotes = bookAnalysisToDelete.ParagraphNotes;
            var highlights = bookAnalysisToDelete.Highlights;

            foreach (var analysisNote in analysisNotes)
            {
                analysisNote.Tags.Clear();
                _context.Remove(analysisNote);
            }
            foreach (var chapterNote in chapterNotes)
            {
                chapterNote.Tags.Clear();
                _context.Remove(chapterNote);
            }
            foreach (var paragraphNote in paragraphNotes)
            {
                paragraphNote.Tags.Clear();
                _context.Remove(paragraphNote);
            }
            foreach (var highlight in highlights)
            {
                foreach (var highlightNote in highlight.HighlightNotes)
                {
                    highlightNote.Tags.Clear();
                    _context.Remove(highlightNote);
                }
                highlight.Tags.Clear();
                _context.Remove(highlight);
            }

            _context.Remove(bookAnalysisToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookAnalysis>> GetAnalysisSummariesByHash(string bookHash)
        {
            return _context.Set<BookAnalysis>()
                .Include(b => b.Users)
                .Where(b => b.BookHash == bookHash);
        }

        public override IQueryable<BookAnalysis> QueryWithIncludes(DbSet<BookAnalysis> querry)
        {
            return querry.Include(b => b.Highlights).ThenInclude(h => h.Tags)
                .Include(b => b.Highlights).ThenInclude(h => h.HighlightNotes).ThenInclude(n => n.Tags)
                .Include(b => b.Tags)
                .Include(b => b.ParagraphNotes).ThenInclude(n => n.Tags)
                .Include(b => b.ChapterNotes).ThenInclude(n => n.Tags)
                .Include(b => b.AnalysisNotes).ThenInclude(n => n.Tags)
                .Include(b => b.Users);
        }

        public async Task<BookAnalysis> RetrieveDetailedAnalysisById(int bookAnalysisId)
        {
            var analysis = await _context.Set<BookAnalysis>()
                .Include(b => b.AnalysisNotes)
                .Include(b => b.ChapterNotes)
                .Include(b => b.ParagraphNotes)
                .Include(b => b.Highlights)
                    .ThenInclude(h => h.HighlightNotes)
                 .Include(b => b.Users)
                 .FirstOrDefaultAsync(b => b.Id == bookAnalysisId);

            if (analysis is not null)
            {
                analysis.Tags = await _context.Set<Tag>()
                .Include(t => t.AnalysisNotes)
                .Include(t => t.ChapterNotes)
                .Include(t => t.ParagraphNotes)
                .Include(t => t.Highlights)
                .Include(t => t.HighlightNotes)
                .Where(t => t.BookAnalysisId == bookAnalysisId).ToListAsync();
            }

            return analysis;
        }
    }
}