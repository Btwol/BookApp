namespace BookApp.Server.Services.MapperServices
{
    public class MapperService<D, C> : IMapperService<D, C> where D : IDbModel where C : IClientModel
    {
        protected readonly IMapper _mapper;

        public MapperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public virtual C MapToClientModel(D dbModel)
        {
            return _mapper.Map<C>(dbModel);
        }

        public virtual D MapToDbModel(C clientModel)
        {
            return _mapper.Map<D>(clientModel);
        }
    }
}
