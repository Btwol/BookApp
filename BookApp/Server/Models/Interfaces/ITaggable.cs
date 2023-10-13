namespace BookApp.Server.Models.Interfaces
{
    public interface ITaggable : IDbModel
    {
        public List<Tag> Tags { get; set; }
    }
}
