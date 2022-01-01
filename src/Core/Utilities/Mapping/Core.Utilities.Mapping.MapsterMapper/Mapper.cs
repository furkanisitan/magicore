using MM = MapsterMapper;

namespace Core.Utilities.Mapping.MapsterMapper;

public class Mapper : IMapper
{
    private readonly MM.IMapper _mapper;

    public Mapper(MM.IMapper mapper)
    {
        _mapper = mapper;
    }

    public TDestination Map<TDestination>(object source) =>
        _mapper.Map<TDestination>(source);

    public TDestination Map<TSource, TDestination>(TSource source) =>
        _mapper.Map<TSource, TDestination>(source);
}