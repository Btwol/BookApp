namespace BookApp.Server.Services.Interfaces.MapperServices
{
    public interface IHighlightMapper : IMapperService<Highlight, HighlightModel>
    {
        public void MapEditHighlight(Highlight highlightToUpdate, HighlightModel updatedHighlightModel);
    }
}
