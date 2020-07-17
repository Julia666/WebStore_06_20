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

    {   // ������ ���� ���� ������ � ��������� ����, �� ������ ������������ ������������ �� ����� appsettings.json � ���� �������� ������� ConfigureServices() � Configure()
        private readonly IConfiguration _Configuration; 


        // ��������� ������� ������������ ����� �����������
        public Startup(IConfiguration Configuration) 
        {
            _Configuration = Configuration; // ��������� � ��������� ����
        }

        // ����������: ����� ��������� ��������. ���������� �������� � ��� ��������� ��� ����������� ����� ����� ����������, 
        // ������� ������������ ����������� ����� ����� � ����� ��������� ������ ������-������ � ��������� ���-����������
        // (��������, �������������� � ����� ������, ������������ ���� ����� ������������ �������, ������� ����������� -
        // �� ��� ������������  ������ ����� ���������� � ���� ������ ��������, ������� ��������������� ����� �����).
        // � ���� ����� ����������, ��� ������ ����� �������� ������ ����� ���������� (��������� ������� ������ ���� ���������).
        // ����� ����,��� ��� ������� ���������������� �� ���� ���������������� (����� ���� Configure())
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebStoreDB>(opt =>  // ������������ �������� �� ������ ������ ����������
            opt.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WebStoreFirst.DB;Integrated Security=True"));

            services.AddTransient<WebStoreDBInitializer>();

            services.AddControllersWithViews(opt =>
            {
                //opt.Filters.Add<Filter>();
                //opt.Conventions.Add(); // ����������/��������� ���������� MVC-����������
            }).AddRazorRuntimeCompilation(); // (����� AddMvc)��������� ����� �������� MVC � ��������� �������� ������ ����������

            //services.AddScoped<IEmployeesData, InMemoryEmployeesData>(); // � ��������� �������� ��������� ������, ������������ ���
            services.AddScoped<IEmployeesData, SqlEmployeesData>();
            //services.AddScoped<IProductData, InMemoryProductData>();
            services.AddScoped<IProductData, SqlProductData>();


            // - ������ �� ������� ��������� ����������� ���������� [�������]����������
            //  � ��������� ����������� ����������� ������,������� ��������� ���� ���������
            // services.AddTransient<TInterface, TService>();  // - ������������ ������ ��� ���������� �������
            // services.AddScoped<TInterface, TService>();    // - 1 ����� ������
            // services.AddSingleton<TInterface, TService>();  // - ������������ ������
        }

        // ������������� ���������� �������. ��������� �������, ������� ����� ������������ �������� �����������.
        // ����� � �������������� ������� IApplicationBuilder ������������� ������������������ ����������� ������, 
        // ������� ���������� ������������� ����������� ������������ (������� ���������� ���������� �������� ����������� � ���� �������� -
        // ������ ����������� �������� ����� 1 ���� ��������, �� ��������� ���� �����-�� ����� ���������,
        // ����� ���� ���� �������� ����������� �� ��������� ����� ��������, ����� ����� ������� �������� ��������������� � �������� �������
        // � ���������,������� ������������� �� ������ ����� ��������� ������ �������� � � ����� ��� � ���� ���-�������� ������������ ������������)
        // ����� �������, ������� ������, ���������� � ���������� app  - �� ���������� ��������� ����� ��������.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebStoreDBInitializer db /* IServiceProvider services*/)
        {
            db.Initialize();
            // var employees = services.GetRequiredService<IEmployeesData>(); // ����������� � ��������� �������� (������-���������) ������ ��� ������

            if (env.IsDevelopment()) // ���������� ��� ������������� �� ������ �� ������ ����������
            {
               app.UseDeveloperExceptionPage(); // ������� ��������� ���������� (���� � �������� ��������� ��������� ������� ���������� ������,
                                                 // �� ��� ������ ���������������� ����� �� ����� ������ � ��������������� ������ ��������,
                                                 // � ���������� �� ������ ����������� html �������� � ����������� ��� ����� �� ���)
            }

            // ��������� ����������� ���������, ������� �������� ���������� ����������� ���������� 
            // (����� css, javascript, html ������� ����� ���������� � ����������� ����� wwwroot)
            // ���������� ������������� ��
            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseRouting(); // ���������� ������� �������������

            #region
            
            /*app.UseWelcomePage("/welcome");

            app.Use(async (context, next) => // ������� ������ �������������� ��: 
            // ��� ������� ��������� ������� �������� ������ context (� ����������� � �������� ���������� �
            // ����������� ����������)
            {
                // .... �������� ��� context �� ���������� �������� � ��������
                await next(); // ����� ���������� �������������� �� � �������� (����� ������� next)
                // .... �������� ��� context ����� ���������� �������� � ��������
            });

            app.UseMiddleware<TestMiddleWare>(); */
            
            #endregion

            // ���������� ����������� ������������ �������� ����� ����������. 
            // �������� ����� - �����, ������� �� ����� ������� ����� ����� ����� (localhost:5000/HelloWorld/123/asd/zxc - �� ��� ���� ����� 5000)
            // � ������� ������ ����� ��������� ��������� ������� � ��� (��������). � ���� ����� �� ��� ��� � ����� ����������,
            // ��� ��������� �� ����� ������� ������ ���� ���������
            app.UseEndpoints(endpoints =>  
            {
                endpoints.MapGet("/greetings", async context =>
                {
                    await context.Response.WriteAsync(_Configuration["CustomGreetings"]); // ����� ������ _Configuration � ��������� �� ����
                                                                                          // �������� � ��������� CustomGreetings
                });

                /*
                endpoints.MapGet("/", async context =>  // ��� ��������� ������ (�.�. ���� ������ �� �������) ����� ��������� ��������,
                                                        //  ������� ���������� Hello World (����� �������� ��������� �������, ����� ����� ������� ����������� � � ���� ����� ����������)
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                */

                // ���������� ������� MVC, ������� ����� ������������ �������� ������� � ������� ������������
                // (��������� ������ ������������� ������ ��������� ������� � ���� ����������� 
                // �����������, �������� � ��������� ����� ��������, ��� � ��� ����� �����������
                endpoints.MapControllerRoute( // ��������� ���� ������������
                    name:"default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"); // ������ ������ ������� �� ������� ����� ����������� ����������
                              // ��� ����������� / ��� �������� ����� �����������/ �������� ����� ��������
                              // ���� �� ������ ����������, �� �� ��������� ����� Home
                              // ���� �� ������� ��������, �� �� ��������� ������� ��� ������ Index
                              //���� �� ������ ������������� (�������� ��������), �� ��� ����� ��������

            });
        }
    }
}
