
namespace BookApp.Shared.Data
{
    public class Note
    {
        public Guid NoteId { get; set; }
        public Guid? ParentId { get; set; }
        public Highlight? ParentHighlight { get; set; }
        public int PageNumber { get; set; }
        public string Content { get; set; }

        //public List<Tag> Tags { get; set; }

        public Note(Highlight highlight)
        {
            NoteId = Guid.NewGuid();
            ParentId = Guid.Parse(highlight.ElementId);
            ParentHighlight = highlight;
            PageNumber = highlight.PageNumber;
        }
    }
}
