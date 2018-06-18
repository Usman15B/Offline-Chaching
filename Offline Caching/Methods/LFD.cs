using Offline_Caching;

namespace Offline
{
    public class LFD : Strategy
    {

        public LFD() : base("LFD")
        {
            for (var i = 0; i < Instructions.CacheSize; ++i)
                CalcNextUse(-1, Instructions.Cache[i]);
        }

        private readonly int[] _nextUse = new int[Instructions.CacheSize];

        public override int Apply(int requestIndex)
        {
            var latest = 0;

            for (var i = 0; i < Instructions.CacheSize; ++i)
            {
                if (Instructions.Cache[i] == Instructions.Request[requestIndex])
                    return i;

                else if (_nextUse[i] > _nextUse[latest])
                    latest = i;
            }

            return latest;
        }

        public override void Update(int cachePos, int requestIndex, bool cacheMiss)
        {
            _nextUse[cachePos] = CalcNextUse(requestIndex, Instructions.Cache[cachePos]);
        }

        private int CalcNextUse(int requestPosition, char pageItem)
        {
            
                for (var i = requestPosition + 1; i < Instructions.RequestLength; ++i)
                {
                    if (Instructions.Request[i] == pageItem)
                        return i;
                }

                return Instructions.RequestLength + 1;
        }
    }
}