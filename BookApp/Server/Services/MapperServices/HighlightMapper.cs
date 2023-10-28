namespace BookApp.Server.Services.MapperServices
{
    public class HighlightMapper : MapperService<Highlight, HighlightModel>, IHighlightMapper
    {
        public HighlightMapper(IMapper mapper) : base(mapper)
        {
        }

        public void MapEditHighlight(Highlight highlightToUpdate, HighlightModel updatedHighlightModel)
        {
            highlightToUpdate.LastNodeCharIndex = updatedHighlightModel.LastNodeCharIndex;
            highlightToUpdate.FirstNodeCharIndex = updatedHighlightModel.FirstNodeCharIndex;
            highlightToUpdate.FirstNodeIndex = updatedHighlightModel.FirstNodeIndex;
            highlightToUpdate.LastNodeIndex = updatedHighlightModel.LastNodeIndex;
        }
    }
}
