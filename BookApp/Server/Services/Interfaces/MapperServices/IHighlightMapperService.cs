namespace BookApp.Server.Services.Interfaces.MapperServices
{
    public interface IHighlightMapperService
    {
        public Highlight MapToHighlight(HighlightModel highlightModel);
        public HighlightModel MapToHighlightModel(Highlight highlight);
        public void MapEditHighlight(Highlight highlightToUpdate, HighlightModel updatedHighlightModel);
    }
}
