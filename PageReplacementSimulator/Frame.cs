namespace PageReplacementSimulator
{
    using System;

    public class Frame
    {
        private readonly int? pageNo;

        public Frame(int? pageNo)
        {
            this.pageNo = pageNo;
        }

        public int Page
        {
            get
            {
                var no = this.pageNo;
                if (no != null)
                {
                    return no.Value;
                }

                throw new InvalidOperationException("Frame has no page");
            }
        }

        public bool HasPage
        {
            get { return this.pageNo.HasValue; }
        }
    }
}
