using AutoMapper;

namespace Core.Utilities.Mapping;

public class AutoMapperCore : IMapperCore
{
    private readonly IMapper _mapper;

    public AutoMapperCore(IMapper mapper)
    {
        _mapper = mapper;
    }

    public TDestination Map<TDestination>(object source) =>
        _mapper.Map<TDestination>(source);

    public TDestination Map<TSource, TDestination>(TSource source) =>
        _mapper.Map<TSource, TDestination>(source);

}