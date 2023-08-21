
namespace BookApp.Shared.Data
{
    public class NoteModel
    {
        public int NoteId { get; set; }
        public int? ParentId { get; set; }
        public HighlightModel? ParentHighlight { get; set; }
        public int PageNumber { get; set; }
        public string Content { get; set; }

        //public List<Tag> Tags { get; set; }

        public NoteModel(HighlightModel highlight)
        {
            ParentId = int.Parse(highlight.ElementId);
            ParentHighlight = highlight;
            PageNumber = highlight.PageNumber;
        }
    }
}
