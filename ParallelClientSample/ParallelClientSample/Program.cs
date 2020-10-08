using System;

namespace ParallelClientSample
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    class Program
    {
        static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            try	
            {
                // var tasks = currentIds.Select(id => client.GetUser(id));
                // users.AddRange(await Task.WhenAll(tasks));
                await RunSequentially(client);
                await RunInParallel(client);
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");	
                Console.WriteLine("Message :{0} ",e.Message);
            }
        }

        private static async Task RunSequentially(HttpClient client)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var result1 = await client.GetStringAsync("http://www.contoso.com/");
            var result2 = await client.GetStringAsync("http://www.bing.com/");
            
            stopwatch.Stop();

            // Console.WriteLine($"Result: {result1}");
            // Console.WriteLine($"Result: {result2}");

            Console.WriteLine($"Total time in milliseconds: {stopwatch.ElapsedMilliseconds}");
        }

        private static async Task RunInParallel(HttpClient client)
        {
            List<Task<string>> tasks = new List<Task<string>>();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            tasks.Add(client.GetStringAsync("http://www.contoso.com/"));
            tasks.Add(client.GetStringAsync("http://www.bing.com/"));
            var results = (await Task.WhenAll(tasks));

            stopwatch.Stop();

            // foreach (var result in results)
            // {
            //     Console.WriteLine($"Result: {result}");
            // }

            Console.WriteLine($"Total time in milliseconds: {stopwatch.ElapsedMilliseconds}");
        }
    }
}
