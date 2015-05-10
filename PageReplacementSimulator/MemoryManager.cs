namespace PageReplacementSimulator
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    public sealed class MemoryManager
    {
        private readonly TextWriter outputWriter;
        private readonly Frame[] frames;
        private readonly IFrameStrategy frameStrategy;
        private int lastInterruptIdx = -1;

        private int totalNoPageInterrupt;

        public MemoryManager(int frameNo, IFrameStrategy frameStrategy, TextWriter outputWriter )
        {
            if (outputWriter == null)
            {
                throw new ArgumentNullException("outputWriter");
            }

            this.outputWriter = outputWriter;
            this.frameStrategy = frameStrategy;
            this.frames = new Frame[frameNo];

            for (var i = 0; i < frameNo; i++)
            {
                this.frames[i] = new Frame(null);
            }

            this.totalNoPageInterrupt = 0;
        }

        public int TotalInterrputs
        {
            get { return this.totalNoPageInterrupt; }
        }

        public void Load(int pageNo)
        {
            this.lastInterruptIdx = -1;

            try
            {
                this.outputWriter.WriteLine("Requesting page {0}", pageNo);
                this.frameStrategy.PageRequest(pageNo);

                for (var i = 0; i < this.frames.Length; i++)
                {
                    if (!this.frames[i].HasPage || this.frames[i].Page != pageNo)
                    {
                        continue;
                    }

                    return;
                }

                for (var i = 0; i < this.frames.Length; i++)
                {
                    if (this.frames[i].HasPage)
                    {
                        continue;
                    }

                    this.totalNoPageInterrupt += 1;
                    this.lastInterruptIdx = i;

                    this.frames[i] = new Frame(pageNo);
                    this.frameStrategy.PageLoaded(this.frames[i]);
                    return;
                }

                var frameToReplace = this.frameStrategy.FrameToReplace(this.frames);
                this.frames[frameToReplace] = new Frame(pageNo);
                this.frameStrategy.PageLoaded(this.frames[frameToReplace]);

                this.totalNoPageInterrupt += 1;
                this.lastInterruptIdx = frameToReplace;
            }
            finally
            {
                this.DrawFrames();
            }
        }

        private void DrawFrames()
        {
            var maxNumLength = this.frames.Where(f => f.HasPage).Select(f => f.Page).Max().ToString(CultureInfo.InvariantCulture).Length;
            if (maxNumLength == 0)
            {
                maxNumLength = 1;
            }

            var maxLength = maxNumLength + 6;
            for (var i = 0; i < maxLength; i++) this.outputWriter.Write("=");
            this.outputWriter.WriteLine();

            for (var i = 0; i < this.frames.Length; i++)
            {
                var currPage = this.frames[i].HasPage ? this.frames[i].Page.ToString() : "-";

                this.outputWriter.Write("| ");
                this.outputWriter.Write(this.lastInterruptIdx == i ? "({0})" : " {0} ", currPage);

                for (var j = 0; i < maxNumLength - currPage.Length; j++)
                {
                    this.outputWriter.Write(" ");
                }

                this.outputWriter.WriteLine(" |");

                if (i < this.frames.Length - 1)
                {
                    for (var j = 0; j < maxLength; j++) this.outputWriter.Write("-");
                }
                else
                {
                    for (var j = 0; j < maxLength; j++) this.outputWriter.Write("=");
                }
                this.outputWriter.WriteLine();
            }

            this.outputWriter.WriteLine();
        }
    }
}
