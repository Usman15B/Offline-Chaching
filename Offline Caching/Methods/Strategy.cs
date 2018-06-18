namespace Offline
{
    public class Strategy
    {
        protected Strategy(string name)
        {
            StrategyName = name;
        }
        // calculate which cache place should be used
        public virtual int Apply(int requestIndex)
        {
            return 0;
        }

        // updates information the strategy needs
        public virtual void Update(int cachePlace, int requestIndex, bool cacheMiss)
        {
        }

        public string StrategyName { get; set; }

    }
}