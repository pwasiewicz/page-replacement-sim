namespace PageReplacementSimulator.Strategies
{
    using System.Collections.Generic;
    using System.Linq;

    public class FifoFrameStrategy : IFrameStrategy
    {
        private readonly Queue<Frame> framesQueue;

        public FifoFrameStrategy()
        {
            this.framesQueue = new Queue<Frame>();
        }

        public string Name
        {
            get { return "fifo"; }
        }

        public int FrameToReplace(Frame[] availableFrames)
        {
            return availableFrames.ToList().IndexOf(this.framesQueue.Dequeue());
        }

        public void PageLoaded(Frame frame)
        {
            this.framesQueue.Enqueue(frame);
        }

        public void PageRequest(int pageNo)
        {
        }
    }
}
