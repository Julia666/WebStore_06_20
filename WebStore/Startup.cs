using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.DAL.Context;
using WebStore.Data;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.MiddleWare;
using WebStore.Infrastructure.Services;
using WebStore.Infrastructure.Services.InMemory;
using WebStore.Infrastructure.Services.InSQL;

namespace WebStore
{
    public class Startup

    {   // теперь имея этот объект в приватном поле, мы сможем использовать конфигурацию из файла appsettings.json в двух основных методах ConfigureServices() и Configure()
        private readonly IConfiguration _Configuration; 


        // Извлекаем систему конфигурации через конструктор
        public Startup(IConfiguration Configuration) 
        {
            _Configuration = Configuration; // сохраняем в приватное поле
        }

        // Назначение: взять коллекцию сервисов. Необходимо добавить в эту коллекцию все структурные блоки моего приложения, 
        // которые впоследствии объединятся между собой и будут выполнять задачи бизнес-логики и основного веб-приложения
        // (например, взаимодействие с базой данных, отслеживание куда какой пользователь заходит, система логирования -
        // всё это представлено  внутри моего приложения в виде набора сервисов, которые взаимодействуют между собой).
        // И этот метод определяет, что именно будет работать внутри моего приложения (добавляем сервисы внутрь этой коллекции).
        // После того,как все сервисы зарегистрированы их надо сконфигурировать (метод ниже Configure())
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebStoreDB>(opt =>  // регистрируем контекст БД внутри нашего приложения
            opt.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WebStoreFirst.DB;Integrated Security=True"));

            services.AddTransient<WebStoreDBInitializer>();

            services.AddControllersWithViews(opt =>
            {
                //opt.Filters.Add<Filter>();
                //opt.Conventions.Add(); // добавление/изменение соглашений MVC-приложения
            }).AddRazorRuntimeCompilation(); // (ранее AddMvc)добавляем набор сервисов MVC в коллекцию сервисов нашего приложения

            //services.AddScoped<IEmployeesData, InMemoryEmployeesData>(); // в коллекцию сервисов добавляем сервис, регистрируем его
            services.AddScoped<IEmployeesData, SqlEmployeesData>();
            //services.AddScoped<IProductData, InMemoryProductData>();
            services.AddScoped<IProductData, SqlProductData>();


            // - каждый из методов выполняет регистрацию указанного [сервиса]интерфейса
            //  с указанной реализацией конкретного класса,который выполняет этот интерфейс
            // services.AddTransient<TInterface, TService>();  // - генерируются каждый раз уникальные объекты
            // services.AddScoped<TInterface, TService>();    // - 1 общий объект
            // services.AddSingleton<TInterface, TService>();  // - единственный объект
        }

        // Конфигурирует конкретные сервисы. Формирует конвеер, который будет обрабатывать входящие подключения.
        // Здесь с использованием объекта IApplicationBuilder выстраивается последовательность специальных блоков, 
        // которые называются промежуточным программным обеспечением (которые занимаются обработкой входящих подключений в виде конвеера -
        // сперва подключение проходит через 1 блок конвеера, он выполняет свою какую-то часть обработки,
        // затем этот блок передает подключение на следующее звено конвеера, после этого конвеер начинает разворачиваться в обратную сторону
        // и результат,который сформировался на каждом этапе обрастает новыми деталями и в конце уже в виде веб-страницы отправляется пользователю)
        // Таким образом, вызывая методы, обращенные к переменной app  - мы наращиваем структуру этого конвеера.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebStoreDBInitializer db /* IServiceProvider services*/)
        {
            db.Initialize();
            // var employees = services.GetRequiredService<IEmployeesData>(); // запрашиваем у менеджера сервисов (сервис-провайдер) нужный нам сервис

            if (env.IsDevelopment()) // подключаем это промежуточное ПО только на стадии разработки
            {
               app.UseDeveloperExceptionPage(); // система обработки исключений (если в процессе обработки входящего запроса происходит ошибка,
                                                 // то эта ошибка распространяется вверх по стеку вызова и перехватывается данной системой,
                                                 // в результате мы увидим специальную html страницу с информацией что пошло не так)
            }

            // добавляем специальные механизмы, которые способны возвращать статическое содержимое 
            // (файлы css, javascript, html которые могут находиться в специальной папке wwwroot)
            // подключаем промежуточное ПО
            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseRouting(); // подключени системы маршрутизации

            #region
            
            /*app.UseWelcomePage("/welcome");

            app.Use(async (context, next) => // Принцип работы промежуточного ПО: 
            // для каждого входящего запроса получаем объект context (с информацией о входящем соединении и
            // формируемом результате)
            {
                // .... действия над context до следующего элемента в конвеере
                await next(); // вызов следующего промежуточного ПО в конвеере (через делегат next)
                // .... действия над context после следующего элемента в конвеере
            });

            app.UseMiddleware<TestMiddleWare>(); */
            
            #endregion

            // Добавление специальных обработчиков конечных точек приложения. 
            // Конечная точка - адрес, который мы можем вводить после имени хоста (localhost:5000/HelloWorld/123/asd/zxc - всё что идет после 5000)
            // и которая должна будет выполнить обработку запроса к ней (действия). В этом месте мы как раз и можем определить,
            // что конкретно по каким адресам должно быть выполнено
            app.UseEndpoints(endpoints =>  
            {
                endpoints.MapGet("/greetings", async context =>
                {
                    await context.Response.WriteAsync(_Configuration["CustomGreetings"]); // берем объект _Configuration и извлекаем из него
                                                                                          // значение с названием CustomGreetings
                });

                /*
                endpoints.MapGet("/", async context =>  // для корневого адреса (т.е. если ничего не вводить) будет выполнено действие,
                                                        //  которое возвращает Hello World (берет контекст входящего запроса, берет ответ который формируется и в него пишет асинхронно)
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                */

                // подключаем систему MVC, которая будет сопоставлять входящие запросы с именами контроллеров
                // (формируем шаблон сопоставления адреса входящего запроса с теми параметрами 
                // контроллера, действия и параметра этого действия, что у нас будут реализованы
                endpoints.MapControllerRoute( // добавляем путь контроллерам
                    name:"default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"); // шаблон строки запроса из которой будет извлекаться информация
                              // имя контроллера / имя действия этого контроллера/ параметр этого дейтсвия
                              // если не указан контроллер, то по умолчанию брать Home
                              // если не указано действие, то по умолчанию считать его именем Index
                              //если не указан идентификатор (параметр действия), то его можно опустить

            });
        }
    }
}
