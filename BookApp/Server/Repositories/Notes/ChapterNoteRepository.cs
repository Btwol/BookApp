using BookApp.Server.Repositories.Interfaces.Notes;

namespace BookApp.Server.Repositories.Notes
{
    public class ChapterNoteRepository : NoteRepository<ChapterNote>, IChapterNoteRepository
    {
        public ChapterNoteRepository(DataContext context) : base(context) { }
    }
}
