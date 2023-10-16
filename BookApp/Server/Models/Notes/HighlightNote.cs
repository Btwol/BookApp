namespace BookApp.Server.Models.Notes
{
    public class HighlightNote : Note
    {
        public int HighlightId { get; set; }
        public virtual Highlight Highlight { get; set; }
    }
}
