namespace BookApp.Server.Services.Interfaces
{
    public interface IHubServerService
    {
        public Task AnalysisSummaryUpdated(BookAnalysisSummaryModel bookAnalysisSummaryModel);
        public Task AnalysisMemberAdded(int bookAnalysisId, int userId);
        public Task AnalysisMemberModified(int bookAnalysisId, int userId, MemberType memberType);
        public Task AnalysisMemberRemoved(int bookAnalysisId, int userId);
        public Task HighlightAdded(HighlightModel highlight);
        public Task HighlightUpdated(HighlightModel highlight);
        public Task HighlightRemoved(int bookAnalysisId, int highlightId);
        public Task TagAdded(int bookAnalysisId, int tagId, int taggedId, string taggedType);
        public Task TagRemoved(int bookAnalysisId, int tagId, int taggedId, string taggedType);
        public Task TagCreated(int bookAnalysisId, TagModel tagModel);
        public Task TagUpdated(int bookAnalysisId, TagModel tagModel);
        public Task TagDeleted(int bookAnalysisId, int tagId);
        public Task NoteCreated(int bookAnalysisId, INoteClientModel noteModel);
        public Task NoteUpdated(int bookAnalysisId, INoteClientModel noteModel);
        public Task NoteDeleted(int bookAnalysisId, int noteId, string noteType);
    }
}
