namespace BookApp.Server.Services.Interfaces.MapperServices
{
    public interface INoteMapperService<D, C> : IMapperService<D, C> where D : INote where C : INoteModel
    {
    }
}
