using Offline_Caching;

namespace Offline
{
    public class Lifo : Strategy
    {

        public Lifo() : base("Lifo")
        {
            for (var i = 0; i < Instructions.CacheSize; ++i) _age[i] = 0;
        }

        private readonly int[] _age = new int[Instructions.CacheSize];

        public override int Apply(int requestIndex)
        {
            var newest = 0;

            for (var i = 0; i < Instructions.CacheSize; ++i)
            {
                if (Instructions.Cache[i] == Instructions.Request[requestIndex])
                    return i;

                else if (_age[i] < _age[newest])
                    newest = i;
            }

            return newest;
        }

        public override void Update(int cachePos, int requestIndex, bool cacheMiss)
        {
            // nothing changed we dont need to update the ages
            if (!cacheMiss)
                return;

            // all old pages get older, the new one get 0
            for (var i = 0; i < Instructions.CacheSize; ++i)
            {
                if (i != cachePos)
                    _age[i]++;

                else
                    _age[i] = 0;
            }
        }
    }
}