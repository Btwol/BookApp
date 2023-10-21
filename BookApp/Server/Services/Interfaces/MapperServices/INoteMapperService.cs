namespace BookApp.Server.Services.Interfaces.MapperServices
{
    public interface INoteMapperService<D, C> : IMapperService<D, C> where D : INoteDBModel where C : INoteClientModel
    {
    }
}
