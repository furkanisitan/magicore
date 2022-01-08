using AM = AutoMapper;

namespace Core.Utilities.Mapping.AutoMapper;

public class Mapper : IMapper
{
    private readonly AM.IMapper _mapper;

    public Mapper(AM.IMapper mapper)
    {
        _mapper = mapper;
    }

    public TDestination Map<TDestination>(object? source) =>
        _mapper.Map<TDestination>(source);

    public TDestination Map<TSource, TDestination>(TSource source) =>
        _mapper.Map<TSource, TDestination>(source);
}