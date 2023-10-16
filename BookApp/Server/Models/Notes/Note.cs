namespace BookApp.Server.Models.Notes
{
    public abstract class Note : INoteDBModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public virtual List<Tag> Tags { get; set; } = new();
    }
}
