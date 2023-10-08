namespace BookApp.Server.Services.MapperServices.Mappings
{
    public class AnalysisNoteMapperProfile : Profile
    {
        public AnalysisNoteMapperProfile()
        {
            CreateMap<AnalysisNoteModel, AnalysisNote>().ReverseMap();
        }
    }
}
