using BookApp.Shared.Interfaces.Model;

namespace BookApp.Shared.Data
{
    public class TagModel : IClientModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
