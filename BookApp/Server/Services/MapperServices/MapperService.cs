namespace BookApp.Server.Services.MapperServices
{
    public class MapperService<D, C> : IMapperService<D, C> where D : IDbModel where C : IClientModel
    {
        protected readonly IMapper _mapper;

        public MapperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public virtual async Task<C> MapToClientModel(D dbModel)

        {
            return _mapper.Map<C>(dbModel);
        }

        public virtual async Task<D> MapToDbModel(C clientModel)
        {
            return _mapper.Map<D>(clientModel);
        }
    }
}

