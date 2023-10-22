namespace BookApp.Server.Services.MapperServices
{
    public class AnalysisNoteMapper : MapperService<AnalysisNote, AnalysisNoteModel>, IAnalysisNoteMapper
    {
        public AnalysisNoteMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}
