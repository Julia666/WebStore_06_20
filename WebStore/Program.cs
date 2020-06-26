using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

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
                );
    }
}
 