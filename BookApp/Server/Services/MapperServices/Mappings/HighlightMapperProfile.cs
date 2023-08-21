using AutoMapper;
using BookApp.Server.Models;
using BookApp.Shared.Data;

namespace BookApp.Server.Services.MapperServices.Mappings
{
    public class HighlightMapperProfile : Profile
    {
        public HighlightMapperProfile()
        {
            CreateMap<HighlightModel, Highlight>().ReverseMap();
        }
    }
}