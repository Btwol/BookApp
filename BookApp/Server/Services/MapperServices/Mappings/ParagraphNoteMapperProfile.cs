namespace BookApp.Server.Services.MapperServices.Mappings
{
    public class ParagraphNoteMapperProfile : Profile
    {
        public ParagraphNoteMapperProfile()
        {
            CreateMap<ParagraphNoteModel, ParagraphNote>().ReverseMap();
        }
    }

}
