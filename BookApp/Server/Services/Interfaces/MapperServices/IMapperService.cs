namespace BookApp.Server.Services.Interfaces.MapperServices
{
    public interface IMapperService<D, C> where D : IDbModel where C : IClientModel
    {
        public Task<D> MapToDbModel(C clientModel);
        public Task<C> MapToClientModel(D dbModel);
    }
}
