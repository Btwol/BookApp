using BookApp.Server.Services.MapperServices;

namespace BookApp.Server.Services.Interfaces.MapperServices
{
    public interface IMapperService<D, C> where D : IDbModel where C : IClientModel
    {
        public D MapToDbModel(C clientModel);
        public C MapToClientModel(D dbModel);
    }
}
