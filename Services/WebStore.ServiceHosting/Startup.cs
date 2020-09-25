using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces.Services;
using WebStore.Services.Data;
using WebStore.Services.Products;
using WebStore.Services.Products.InCookies;
using WebStore.Services.Products.InSQL;

namespace WebStore.ServiceHosting
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;
         public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebStoreDB>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<WebStoreDBInitializer>();

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<WebStoreDB>() // указываем,где система должна хранить данные (внтри приложения м.б. несколько контекстов БД)
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(opt =>  // конфигурация системы Identity
            {
#if DEBUG     // чтобы выполнялось только в режиме отладки, т.к. пароль теперь небезопасен
                opt.Password.RequiredLength = 3; // требования к паролю (длина)
                opt.Password.RequireDigit = false; // убираем требование, чтобы были цифры
                opt.Password.RequireLowercase = false; // убираем требование, чтобы были буквы нижнего регистра
                opt.Password.RequireUppercase = false; // убираем требование, чтобы были буквы верхнего регистра
                opt.Password.RequireNonAlphanumeric = false; // убираем требование, чтобы были неалфавитные символы
                opt.Password.RequiredUniqueChars = 3;// количество уникальных символов в пароле
#endif
                opt.User.RequireUniqueEmail = false;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

                opt.Lockout.AllowedForNewUsers = true;  // политика блокировки(все вновь создаваемые пользователи д.б. разблокированы)
                opt.Lockout.MaxFailedAccessAttempts = 10;// количество некорректных входов в систему, после которого он будет заблокирован
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10); //насколько именно заблокирован
            });


            // services.AddScoped<IEmployeesData, SqlEmployeesData>();
            // services.AddScoped<IProductData, SqlProductData>();
            // services.AddScoped<IOrderService, SqlOrderService>();
            // services.AddScoped<ICartService, CookiesCartService>();
            services.AddWebStoreServices();

           // Сервис нужен для работы корзины
           services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

           services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
