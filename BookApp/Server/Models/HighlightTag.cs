namespace BookApp.Server.Models
{
    public class HighlightTag : Tag
    {
        public virtual List<Highlight> TaggedHighlights { get; set; }
    }
}
