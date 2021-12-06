namespace Core.Utilities.Mapping;

public class AutoMapperCore : IMapper
{
    private readonly AutoMapper.IMapper _mapper;

    public AutoMapperCore(AutoMapper.IMapper mapper)
    {
        _mapper = mapper;
    }

    public TDestination Map<TDestination>(object source) =>
        _mapper.Map<TDestination>(source);

    public TDestination Map<TSource, TDestination>(TSource source) =>
        _mapper.Map<TSource, TDestination>(source);

}