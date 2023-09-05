namespace BookApp.Server.Services.MapperServices
{
    public class HighlightMapperService : IHighlightMapperService
    {
        private readonly IMapper _mapper;

        public HighlightMapperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void MapEditHighlight(Highlight highlightToUpdate, HighlightModel updatedHighlightModel)
        {
            highlightToUpdate.LastNodeCharIndex = updatedHighlightModel.LastNodeCharIndex;
            highlightToUpdate.FirstNodeCharIndex = updatedHighlightModel.FirstNodeCharIndex;
            highlightToUpdate.PageNumber = updatedHighlightModel.PageNumber;
            highlightToUpdate.FirstNodeIndex = updatedHighlightModel.FirstNodeIndex;
            highlightToUpdate.LastNodeIndex = updatedHighlightModel.LastNodeIndex;
        }

        public Highlight MapToHighlight(HighlightModel highlightModel)
        {
            return _mapper.Map<Highlight>(highlightModel);
        }

        public HighlightModel MapToHighlightModel(Highlight highlight)
        {
            return _mapper.Map<HighlightModel>(highlight);
        }
    }
}
