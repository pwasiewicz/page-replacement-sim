using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PageReplacementSimulator
{
    using System.Runtime.CompilerServices;
    using Nakov.IO;
    using Strategies;

    class Program
    {
        private static IFrameStrategy[] frameStrategies;


        static void Main(string[] args)
        {
            Console.Write("Frames? ");
            var frames = Cin.NextInt();
            if (frames < 1)
            {
                Console.WriteLine("Frame no should be > 0.");
                return;
            }

            Console.Write("Page requests? ");
            var requests = Cin.NextInt();
            if (requests < 1)
            {
                Console.WriteLine("Requests no should be > 0.");
                return;
            }

            Console.WriteLine("\n\rPage requests?");
            var list = new List<int>();

            for (var i = 0; i < requests; i++) list.Add(Cin.NextInt());

            frameStrategies = new IFrameStrategy[]
                                  {
                                      new FifoFrameStrategy(), 
                                      new LruStrategy(), 
                                      new OptimalStrategy(list)
                                  };

            Console.Write("Strategy (lru, fifo, optimal)? ");
            var strategy = Cin.NextToken();
            Console.WriteLine();

            var frameStrat = frameStrategies.SingleOrDefault(s => s.Name.Equals(strategy));
            if (frameStrat == null)
            {
                Console.WriteLine("Invalid strategy.");
                return;
            }

            var mgr = new MemoryManager(frames, frameStrat, Console.Out);

            list.ForEach(req => mgr.Load(req));

            Console.WriteLine();
            Console.WriteLine("Total interrupts: {0}", mgr.TotalInterrputs);
        }
    }
}
