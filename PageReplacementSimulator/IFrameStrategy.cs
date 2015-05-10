namespace PageReplacementSimulator
{
    public interface IFrameStrategy
    {
        string Name { get; }

        int FrameToReplace(Frame[] availableFrames);

        void PageLoaded(Frame frame);

        void PageRequest(int pageNo);
    }
}
