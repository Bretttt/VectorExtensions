namespace VectorExtensions
{
    internal static class Reinterpret
    {
        public static unsafe TDestination Cast<TSource, TDestination>(TSource value)
            where TSource : unmanaged
            where TDestination : unmanaged 
            => *(TDestination*)&value;
    }
}