using BookApp.Shared.Interfaces.Model;

namespace BookApp.Shared.Models.ClientModels.Notes
{
    public class ChapterNoteModel : NoteModel, IBoundToChapter
    {
        public int Chapter { get; set; }
    }
}
