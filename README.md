# Proxy Checker

```csharp

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
```
**https://github.com/proxifly/free-proxy-list/blob/main/proxies/all/data.txt**
