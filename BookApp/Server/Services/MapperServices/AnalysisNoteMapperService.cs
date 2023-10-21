namespace BookApp.Server.Services.MapperServices
{
    public class AnalysisNoteMapperService : MapperService<AnalysisNote, AnalysisNoteModel>, IAnalysisNoteMapperService
    {
        public AnalysisNoteMapperService(IMapper mapper) : base(mapper)
        {
        }
    }
}
