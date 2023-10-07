namespace BookApp.Server.Models.Notes
{
    public abstract class Note : INote
    {
        public int Id { get; set; }
        public int BookAnalysisId { get; set; }
        public string Content { get; set; }
    }
}
