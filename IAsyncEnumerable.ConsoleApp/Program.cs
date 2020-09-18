using IAsyncEnumerable.ConsoleApp.RangeAsyncNS;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IAsyncEnumerable.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Press 'Q' key to Quit!");
            var cancellationTokenSource = new CancellationTokenSource();
            var userQuitTask = AwaitUserQuitAsync();

            var task = RangeAsync(cancellationTokenSource.Token);
            
            if (await Task.WhenAny(task, userQuitTask) == userQuitTask)
                Console.WriteLine("User quit requested");
            else
                Console.WriteLine("Task Finished Successfully");

            cancellationTokenSource.Cancel();
            Console.WriteLine("Bye World!");
        }

        private static async Task RangeAsync(CancellationToken token = default)
        {
            await foreach (var item in new RangeAsync(1, 10).WithCancellation(token))
            {
                Console.WriteLine(item);
            }
        }

        private static async Task AwaitUserQuitAsync()
        {
            await Task.Run(() => 
            {
                while (Console.ReadKey().Key != ConsoleKey.Q) ;
            });
        }
    }
}
