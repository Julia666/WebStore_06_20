using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

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
                        }))
        
                .UseSerilog((host, log) => log.ReadFrom.Configuration(host.Configuration)
                    .MinimumLevel.Debug() // минимальный уровень ведения журнала - debug
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Error) // переопределение уровня для определенных пространств имен
                    .Enrich.FromLogContext() // указываем,что в процесс логирования необходимо добавить информацию из контекста
                    .WriteTo.Console(
                        outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level:u3}]{SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}")  // указываем, куда писать
                    .WriteTo.RollingFile($@".\Log\WebStore[{DateTime.Now:yyyy-mm-ddTHH-mm-ss}].log")
                    .WriteTo.File(new JsonFormatter(",", true), $@".\Log\WebStore[{DateTime.Now:yyyy-mm-ddTHH-mm-ss}].log.json")
                    .WriteTo.Seq("http://localhost:5341")
                ) ;

        //.UseUrls("http://localhost:5000") // можно так, через метод расширени
        // открывает наше приложение миру, какие именно адреса система будет прослушивать
        // если не подключить - не будет доступа извне к приложению, например, с телефона

    }
}
 