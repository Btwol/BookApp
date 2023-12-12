namespace BookApp.Server.Services.MapperServices.Mappings
{
    public class AnalysisVersionProfile : Profile
    {
        public AnalysisVersionProfile()
        {
            CreateMap<AnalysisVersion, AnalysisVersionModel>();
            CreateMap<AnalysisVersionModel, AnalysisVersion>();
        }
    }
}
