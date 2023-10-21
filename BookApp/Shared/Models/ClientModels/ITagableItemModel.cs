using BookApp.Shared.Interfaces.Model;

namespace BookApp.Shared.Models.ClientModels
{
    public interface ITagableItemModel : IClientModel
    {
        public int Id { get; set; }
        public List<TagModel> Tags { get; set; }
    }
}
