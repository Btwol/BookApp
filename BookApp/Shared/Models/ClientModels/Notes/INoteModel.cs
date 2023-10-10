using BookApp.Shared.Interfaces.Model;

namespace BookApp.Shared.Models.ClientModels.Notes
{
    public interface INoteClientModel : IClientModel, INote
    {
        public int Id { get; set; }
    }
}
