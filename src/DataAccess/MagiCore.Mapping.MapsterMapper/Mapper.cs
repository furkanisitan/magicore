using MM = MapsterMapper;

namespace MagiCore.Mapping.MapsterMapper;

public class Mapper : IMapper
{
    private readonly MM.IMapper _mapper;

    public Mapper(MM.IMapper mapper)
    {
        _mapper = mapper;
    }

    public TDestination Map<TSource, TDestination>(TSource source) =>
        _mapper.Map<TSource, TDestination>(source);
}