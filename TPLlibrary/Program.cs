using System;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        int numThreads = Environment.ProcessorCount; 
        if (args.Length > 0)
        {
            if (int.TryParse(args[0], out int threadCount) && threadCount > 0)
            {
                numThreads = threadCount;
            }
            else
            {
                Console.WriteLine("Invalid command line argument for thread count. Number of available processor cores used.");
            }
        }

        int[] numbers = Enumerable.Range(1, 1000000).ToArray();
        long sum = 0;

        var watch = System.Diagnostics.Stopwatch.StartNew();

  
        Parallel.ForEach(numbers, new ParallelOptions { MaxDegreeOfParallelism = numThreads }, (number) =>
        {
            long square = Calculate(number);
            Interlocked.Add(ref sum, square); 
        });

        watch.Stop();

        Console.WriteLine($"Sum: {sum}");
        Console.WriteLine($"Execution time: {watch.ElapsedMilliseconds} ms");
    }

    static long Calculate(int number)
    {
        return (long)number * number;
    }
}
