namespace PageReplacementSimulator.Strategies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class OptimalStrategy : IFrameStrategy
    {
        private readonly Queue<int> pageFutureRequests;

        public OptimalStrategy(IEnumerable<int> pageFutureRequests)
        {
            this.pageFutureRequests = new Queue<int>(pageFutureRequests);
        }

        public string Name
        {
            get { return "optimal"; }
        }

        public int FrameToReplace(Frame[] availableFrames)
        {
            var futurePages = this.pageFutureRequests.ToList();
            var framesPages = availableFrames.Select(f => f.Page).ToList();

            var candidate = framesPages[0];
            var candidateWaits = futurePages.IndexOf(candidate);

            if (candidateWaits == -1)
            {
                return 0;
            }

            for (var i = 1; i < framesPages.Count; i++)
            {
                var currWaits = futurePages.IndexOf(framesPages[i]);
                if (currWaits == -1) return i;

                if (currWaits <= candidateWaits)
                {
                    continue;
                }

                candidate = i;
                candidateWaits = currWaits;
            }

            return candidate;
        }

        public void PageLoaded(Frame frame)
        {
        }

        public void PageRequest(int pageNo)
        {
            var firstInQueue = this.pageFutureRequests.Dequeue();
            if (firstInQueue != pageNo)
            {
                throw new InvalidOperationException("Invalid, non-preticable request.");
            }
        }
    }
}
