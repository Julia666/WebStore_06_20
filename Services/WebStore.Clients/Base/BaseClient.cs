using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace WebStore.Clients.Base
{
    public abstract class BaseClient
    {
        protected readonly string _ServiceAddress;
        protected readonly HttpClient _Client;


       // Будет общий адресс всего нашего хостинга и для каждого клиента внутри этого хостинга будет определен локальный
       // адрес конкретного сервиса. Адрес хостинга будет храниться в - Configuration, а адрес сервиса будет определяться наследником - ServiceAddress
       protected BaseClient(IConfiguration Configuration, string ServiceAddress)
        {
            _ServiceAddress = ServiceAddress;
            _Client = new HttpClient
            {
               BaseAddress = new Uri(Configuration["WebApiURL"]),// адрес хостинга,который возьмем из конфигурации
               DefaultRequestHeaders =
               { // указываем клиенту заголовки, которые он будет отправлять хостингу WebApi, которые будут говорить о том,
                 // в каком формате хостинг должен выдавать данные
                   Accept = { new MediaTypeWithQualityHeaderValue("application/json")}
               }
            };
        }
    }
}
