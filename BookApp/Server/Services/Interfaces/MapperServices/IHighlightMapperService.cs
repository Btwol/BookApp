using BookApp.Shared.Models.ClientModels;

namespace BookApp.Server.Services.Interfaces.MapperServices
{
    public interface IHighlightMapperService : IMapperService<Highlight, HighlightModel>
    {
        public void MapEditHighlight(Highlight highlightToUpdate, HighlightModel updatedHighlightModel);
    }
}
