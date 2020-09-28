using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WebStore.Clients.Base
{
    public abstract class BaseClient : IDisposable
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


       protected T Get<T>(string url) => GetAsync<T>(url).Result;
       protected async Task<T> GetAsync<T>(string url)
       {
           var response = await _Client.GetAsync(url);
           return await response
               .EnsureSuccessStatusCode() // Убеждаемся, что в ответ получен 200-ый статусный код
               .Content        // В ответе есть содержимое, с которым можно работать
               .ReadAsAsync<T>(); // Десериализуем данные в нужный тип объекта
       }

       protected HttpResponseMessage Post<T>(string url, T item) => PostAsync(url, item).Result;
       protected async Task<HttpResponseMessage> PostAsync<T>(string url, T item)
       {
           var response = await _Client.PostAsJsonAsync(url, item);
           return response.EnsureSuccessStatusCode();
       }


       protected HttpResponseMessage Put<T>(string url, T item) => PutAsync(url, item).Result;
       protected async Task<HttpResponseMessage> PutAsync<T>(string url, T item)
       {
           var response = await _Client.PutAsJsonAsync(url, item);
           return response.EnsureSuccessStatusCode();
       }


       protected HttpResponseMessage Delete(string url) => DeleteAsync(url).Result;
       protected async Task<HttpResponseMessage> DeleteAsync(string url)
       {
           var response = await _Client.DeleteAsync(url);
           return response;
       }

        #region IDisposable

        // ~ BaseClient() => Dispose(false); - деструктор
        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            // Здесь можно выполнить освобождение неуправляемых ресурсов
            if (disposing)
            {
                // Здесь можно выполнить освобождение управляемых ресурсов
                _Client.Dispose();
            }
        }

        #endregion
    }
}
