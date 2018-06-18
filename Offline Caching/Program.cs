using System;
using Offline;

namespace Offline_Caching
{
    class Program
    {
        static void Main(string[] args)
        {
            Strategy[] selectedStrategy = 
                { new Fifo(), new Lifo(), new LFD() };

            for (var strat = 0; strat < 3; ++strat)
            {
                // reset cache
                for (var i = 0; i < Instructions.CacheSize; ++i)
                    Instructions.Cache[i] = Instructions.OriginalCache[i];

                Console.Write("\nStrategy: " + selectedStrategy[strat].StrategyName + "\n");

                Console.Write("\nCache initial: (");
                for (var i = 0; i < Instructions.CacheSize - 1; ++i)
                    Console.Write(Instructions.Cache[i] + ",");
                Console.Write(Instructions.Cache[Instructions.CacheSize - 1] + ")\n\n");

                Console.Write("Request\t");

                for (var i = 0; i < Instructions.CacheSize; ++i)
                    Console.Write("cache" + i + "\t");
                Console.Write("cacheMiss \n");

                var cntMisses = 0;

                for (var i = 0; i < Instructions.RequestLength; ++i)
                {
                    var isMiss = Instructions.UpdateCache(i, selectedStrategy[strat]);
                    if (isMiss) ++cntMisses;

                    Console.Write("  " + Instructions.Request[i] + "\t");
                    for (var l = 0; l < Instructions.CacheSize; ++l)
                        Console.Write("  " + Instructions.Cache[l] + "\t");
                    Console.Write((isMiss ? "x" : "" ));
                    Console.WriteLine();
                }

                Console.Write("\nTotal cache misses: " + cntMisses + "\n");

            }

            Console.WriteLine("\nThe greedy strategy LFD is indeed the only optimal strategy of the 3 presented.");
        }
    }
}
