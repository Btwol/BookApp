using BookApp.Server.Repositories.Interfaces.Notes;

namespace BookApp.Server.Repositories.Notes
{
    public class AnalysisNoteRepository : NoteRepository<AnalysisNote>, IAnalysisNoteRepository
    {
        public AnalysisNoteRepository(DataContext context) : base(context) { }
    }
}
