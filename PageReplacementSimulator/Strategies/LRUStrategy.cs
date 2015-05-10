namespace PageReplacementSimulator.Strategies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LruStrategy : IFrameStrategy
    {
        private readonly IDictionary<Frame, int> frameUsage;

        public LruStrategy()
        {
            this.frameUsage = new Dictionary<Frame, int>();
        }

        public string Name
        {
            get { return "lru"; }
        }

        public int FrameToReplace(Frame[] availableFrames)
        {
            if (availableFrames.Any(f => !this.frameUsage.ContainsKey(f))) {
                throw new InvalidOperationException("Cannot find frame usage info.");
            }

            var toReplace = this.frameUsage.OrderByDescending(p => p.Value).First();
            this.frameUsage.Remove(toReplace);
            return availableFrames.ToList().IndexOf(toReplace.Key);
        }

        public void PageLoaded(Frame frame)
        {
            this.frameUsage.Add(frame, 0);
        }

        public void PageRequest(int pageNo)
        {
            var key = this.frameUsage.SingleOrDefault(p => p.Key.Page == pageNo).Key;
            if (key != null)
            {
                this.frameUsage[key] = 0;
            }

            foreach (var localKey in this.frameUsage.Keys.ToList())
            {
                if (localKey != key)
                {
                    this.frameUsage[localKey] += 1;
                }
            }
        }
    }
}
