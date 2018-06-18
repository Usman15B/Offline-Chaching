using Offline;

namespace Offline_Caching
{
    public static class Instructions
    {
        public static int CacheSize { get; } = 3;
        public static int RequestLength { get; } = 16;

        public static char[] Request { get; } =
            {'a', 'a', 'd', 'e', 'b', 'b', 'a', 'c', 'f', 'd', 'e', 'a', 'f', 'b', 'e', 'c'};

        public static char[] Cache { get; } = {'a', 'b', 'c'};

        // for reset
        public static char[] OriginalCache { get; } = {'a', 'b', 'c'};

        public static bool UpdateCache(int requestIndex,Strategy strategy)
        {
            // calculate where to put request
            var cachePlace = strategy.Apply(requestIndex);

            // proof whether its a cache hit or a cache miss
            var isMiss = Request[requestIndex] != Cache[cachePlace];

            // update strategy (for example recount distances)
            strategy.Update(cachePlace, requestIndex, isMiss);

            // write to cache
            Cache[cachePlace] = Request[requestIndex];

            return isMiss;
        }
    }
}