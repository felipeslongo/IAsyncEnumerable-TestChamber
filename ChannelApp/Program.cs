using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ChannelApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var channel = Channel.CreateBounded<int>(new BoundedChannelOptions(10)
            {
            });

            _ = Task.Run(async delegate 
            {
                foreach (var item in Enumerable.Range(1, 100))
                {                    
                    await channel.Writer.WriteAsync(item);
                    Console.WriteLine($"Producer: {item}");
                }

                //channel.Writer.Complete();
            });

            await foreach (var item in channel.Reader.ReadAllAsync())
            {
                await Task.Delay(100);
                Console.WriteLine($"Consumer: {item}");
            }

            Console.WriteLine("Bye World!");
        }

        public class Data
        {
            public int Propriedade1 { get; set; }
            public string Propriedade2 { get; set; } = string.Empty;
        }

        public static Task<Data> ObterData()
        {

        }
    }
}
