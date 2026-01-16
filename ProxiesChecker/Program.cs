
using System;
using System.IO;


using System.Net;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Threading;


namespace ProxiesChecker
{ 
   

    class Proxyservice
    {
        public static int alive;
        private string ProxyPath;
      
        public Proxyservice(string path)
        {
            this.ProxyPath = path;

        }
      
        
        public string[] loadProxies()
        {
            if (!File.Exists(ProxyPath))
            {
                Console.WriteLine($"Unable to load file from ({ProxyPath}) ");
                return [];
            } else
            {
                return File.ReadAllLines(ProxyPath);
            }
        }
        public async Task check_proxy(string[] proxylist)
        {
            List<Task> tasks = new List<Task>();
            string dump = "";
            for (int i = 0; i < proxylist.Length; i++)
            {
                string proxy = proxylist[i];
                string[] parse = proxy.Split("://");
                string protocol = parse[0];
                string[] address = parse[1].Split(":");
                string ip = address[0];
                string port = address[1];

                switch (protocol.ToLower())
                {
                    case "http":
                        {
                            Task process = Task.Run(async () =>
                            {
                                var sw = System.Diagnostics.Stopwatch.StartNew();
                                bool hidup = await http_proxy_check(ip, int.Parse(port));
                                if (hidup)
                                {
                                    alive++;
                                    Console.WriteLine($"[HTTP] Alive {ip}:{port} | {sw.Elapsed.TotalSeconds:F2} seconds");
                                    dump += $"http://{ip}:{port}\n";
                                }
                            });
                            tasks.Add(process); 
                        } break;

                    default:
                    {

                    } break;
                }
            }
            await Task.WhenAll(tasks);
            File.WriteAllText("active_proxy.txt", dump);
            Console.WriteLine("data has been saved on ./active_proxy.txt");
        }
        public async Task<bool> http_proxy_check(string ip,int port)
        {
            try
            {
                var proxy = new WebProxy($"http://{ip}:{port}");
                var handler = new HttpClientHandler { Proxy = proxy };

                using var client = new HttpClient(handler);
                client.Timeout = TimeSpan.FromSeconds(50);

                var res = await client.GetAsync("https://api.ipify.org");
                return res.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }



    }
    class Program
    {
        static  async Task Main(string[] args)
        {
            var run = new Proxyservice("data.txt");
            var proxies = run.loadProxies();
            await run.check_proxy(proxies);
            Console.WriteLine($"Process Done, found {Proxyservice.alive} proxies alive");
            Console.ReadKey();
        }
    }
}
