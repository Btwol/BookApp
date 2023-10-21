namespace BookApp.Shared.Models.ClientModels
{
    public interface ITagableItemModel
    {
        public int Id { get; set; }
        public List<TagModel> Tags { get; set; }
    }
}
