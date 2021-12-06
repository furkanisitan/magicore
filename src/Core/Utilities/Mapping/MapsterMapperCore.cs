namespace Core.Utilities.Mapping;

public class MapsterMapperCore : IMapper
{
    private readonly MapsterMapper.IMapper _mapper;

    public MapsterMapperCore(MapsterMapper.IMapper mapper)
    {
        _mapper = mapper;
    }

    public TDestination Map<TDestination>(object source) =>
        _mapper.Map<TDestination>(source);

    public TDestination Map<TSource, TDestination>(TSource source) =>
        _mapper.Map<TSource, TDestination>(source);

}