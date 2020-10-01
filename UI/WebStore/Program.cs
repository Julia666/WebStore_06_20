using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace WebStore
{
    public class Program
    {
        public static void Main(string[] args) =>  CreateHostBuilder(args).Build().Run(); 
        // Run() - запускает хост на прослушивание (открывается порт в сетевой карте и хост начинает ловить входящие подключения)

        // Создание объекта-построителя хоста
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(host =>host
                    .UseStartup<Startup>()
                    .ConfigureLogging((host, log) =>
                        {
                            //log.ClearProviders()
                            //log.AddProvider()
                            //log.AddConsole(opt => opt.IncludeScopes = true)
                            //log.AddFilter(level => level >= LogLevel.Information);
                            log.AddFilter("Microsoft",level => level > LogLevel.Warning);
                        }));

        //.UseUrls("http://localhost:5000") // можно так, через метод расширени
        // открывает наше приложение миру, какие именно адреса система будет прослушивать
        // если не подключить - не будет доступа извне к приложению, например, с телефона

    }
}
 