namespace BookApp.Server.Services.MapperServices.Mappings
{
    public class BookAnalysisMapperProfile : Profile
    {
        private static readonly string BreakSign = "_SEPARATE_";
        public BookAnalysisMapperProfile()
        {
            CreateMap<BookAnalysisDetailedModel, BookAnalysis>()
                .ForMember(m => m.Authors, b => b.Ignore())
                .AfterMap((src, dest) => dest.Authors = string.Join(BreakSign, src.Authors));

            CreateMap<BookAnalysisSummaryModel, BookAnalysis>()
                .ForMember(m => m.Authors, b => b.Ignore())
                .AfterMap((src, dest) => dest.Authors = string.Join(BreakSign, src.Authors));

            CreateMap<BookAnalysis, BookAnalysisDetailedModel>()
                .ForMember(m => m.Members, b => b.Ignore())
                .ForMember(m => m.Authors, b => b.Ignore())
                .AfterMap((src, dest) => dest.Authors = src.Authors.Split(new string[] { BreakSign }, StringSplitOptions.None).ToList());

            CreateMap<BookAnalysis, BookAnalysisSummaryModel>()
                .ForMember(m => m.Members, b => b.Ignore())
                .ForMember(m => m.Authors, b => b.Ignore())
                .AfterMap((src, dest) => dest.Authors = src.Authors.Split(new string[] { BreakSign }, StringSplitOptions.None).ToList());
        }
    }
}