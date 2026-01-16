# Proxy Checker
Simple proxy checker built in c#

# Requirements & library
- **System.Net**

# Project To DO
- [ ] Socks (socks4 & socks5)
- [x] HTTP Proxy

![image]([cool thing.png](https://github.com/galviy/proxy-checker/blob/main/cool%20thing.png?raw=true))

- Added some cool response time which can be used as an indicator of the proxy's speed. 

# Modification
you can modify the `proxy.txt` file to whatever you want here
```csharp

 class Program
 {
     static  async Task Main(string[] args)
     {
         var run = new Proxyservice("data.txt"); // <-- change this guy
         var proxies = run.loadProxies();
         await run.check_proxy(proxies);
         Console.WriteLine($"Process Done, found {Proxyservice.alive} proxies alive");
         Console.ReadKey();
     }
 }
```

# Example proxy source
**https://github.com/proxifly/free-proxy-list/blob/main/proxies/all/data.txt**
