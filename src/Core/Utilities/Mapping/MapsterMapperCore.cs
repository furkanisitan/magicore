using MapsterMapper;

namespace Core.Utilities.Mapping
{
    public class MapsterMapperCore : IMapperCore
    {
        private readonly IMapper _mapper;

        public MapsterMapperCore(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDestination Map<TSource, TDestination>(TSource source) =>
            _mapper.Map<TDestination>(source);

    }
}
