using MM = MapsterMapper;

namespace MagiCore.Mapping.MapsterMapper;

public class Mapper : IMapper
{
    private readonly MM.IMapper _mapper;

    public Mapper(MM.IMapper mapper)
    {
        _mapper = mapper;
    }

    public TDestination Map<TDestination>(object? source) =>
        _mapper.Map<TDestination>(source);

    public TDestination Map<TSource, TDestination>(TSource source) =>
        _mapper.Map<TSource, TDestination>(source);

    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination) =>
        _mapper.Map(source, destination);

    public object Map(object source, Type sourceType, Type destinationType) =>
        _mapper.Map(source, sourceType, destinationType);

    public object Map(object source, object destination, Type sourceType, Type destinationType) =>
        _mapper.Map(source, destination, sourceType, destinationType);
}