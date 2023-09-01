namespace BookApp.Server.Services.MapperServices.Mappings
{
    public class BookAnalysisMapperProfile : Profile
    {
        private static readonly string BreakSign = "_SEPARATE_";
        public BookAnalysisMapperProfile()
        {
            CreateMap<BookAnalysisModel, BookAnalysis>()
                .ForMember(m => m.Authors, b => b.Ignore())
                .AfterMap((src, dest) => dest.Authors = string.Join(BreakSign, src.Authors));

            CreateMap<BookAnalysis, BookAnalysisModel>()
                .ForMember(m => m.Authors, b => b.Ignore())
                .AfterMap((src, dest) => dest.Authors = src.Authors.Split(new string[] { BreakSign }, StringSplitOptions.None).ToList());


            //.ForMember(m => m.Authors, b => b
            //.MapFrom(src => src.Authors.Split(new string[] { ", " }, StringSplitOptions.None).ToList()));
        }
    }
}