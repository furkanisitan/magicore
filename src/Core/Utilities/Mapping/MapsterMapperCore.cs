using Mapster;

namespace Core.Utilities.Mapping
{
    public class MapsterMapperCore : IMapperCore
    {
        public TDestination Map<TSource, TDestination>(TSource source) =>
            source.Adapt<TDestination>();

    }
}
