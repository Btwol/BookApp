using BookApp.Server.Models;
using BookApp.Server.Models.Notes;

namespace BookApp.Server.Repositories
{
    public class BookAnalysisRepository : BaseRepository<BookAnalysis>, IBookAnalysisRepository
    {
        public BookAnalysisRepository(DataContext context) : base(context)
        {

        }

        public async override Task Delete(BookAnalysis bookAnalysisToDelete)
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
    }
}
