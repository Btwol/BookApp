using BookApp.Shared.Models.ClientModels;

namespace BookApp.Server.Services.MapperServices
{
    public class HighlightMapperService : MapperService<Highlight, HighlightModel>, IHighlightMapperService
    {
        public HighlightMapperService(IMapper mapper) : base(mapper)
        {
        }

        public void MapEditHighlight(Highlight highlightToUpdate, HighlightModel updatedHighlightModel)
        {
            highlightToUpdate.LastNodeCharIndex = updatedHighlightModel.LastNodeCharIndex;
            highlightToUpdate.FirstNodeCharIndex = updatedHighlightModel.FirstNodeCharIndex;
            highlightToUpdate.PageNumber = updatedHighlightModel.PageNumber;
            highlightToUpdate.FirstNodeIndex = updatedHighlightModel.FirstNodeIndex;
            highlightToUpdate.LastNodeIndex = updatedHighlightModel.LastNodeIndex;
        }
    }
}
