using BookApp.Shared.Interfaces.Model;

namespace BookApp.Shared.Models.ClientModels.Notes
{
    public class ParagraphNoteModel : NoteModel, IBoundToChapter
    {
        public int TextNodeNumber { get; set; }
        public int Chapter { get; set; }
    }
}
