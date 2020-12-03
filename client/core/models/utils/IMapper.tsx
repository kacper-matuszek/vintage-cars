export default interface IMapper<TSource, TDestination>{
    toDestination(source: TSource): TDestination;
    toSource(destination: TDestination): TSource;
}