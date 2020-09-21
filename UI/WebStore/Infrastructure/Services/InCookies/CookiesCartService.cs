using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Mapping;

namespace WebStore.Infrastructure.Services.InCookies
{
    public class CookiesCartService : ICartService
    {
        private readonly IProductData _ProductData;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly string _CartName;

       private Cart Cart 
        { 
            get
            {
                var context = _HttpContextAccessor.HttpContext;  // берем контекст http-запроса и из него извлекаем cookies
                var cookies = context.Response.Cookies;
                var cart_cookies = context.Request.Cookies[_CartName]; // пытаемся найти саму cookies, которая отвечает за хранение корзины
                if(cart_cookies is null)  // если cookies вообще нет, то создаем новую корзину и сериализуем её
                {
                    var cart = new Cart();
                    cookies.Append(_CartName, JsonConvert.SerializeObject(cart));
                    return cart;
                }

                ReplaceCookies(cookies, cart_cookies); // если cookies была, то её необходимо подменить
                return JsonConvert.DeserializeObject<Cart>(cart_cookies); // десериализуем её
            }
            set => ReplaceCookies(_HttpContextAccessor.HttpContext.Response.Cookies, JsonConvert.SerializeObject(value)); 
        }

        private void ReplaceCookies(IResponseCookies cookies, string cookie)
        {
            cookies.Delete(_CartName);
            cookies.Append(_CartName, cookie);
        }

        public CookiesCartService(IProductData ProductData, IHttpContextAccessor HttpContextAccessor) // HttpContextAccessor извлекает информацию из контекста http-запроса
        {
            _ProductData = ProductData;
            _HttpContextAccessor = HttpContextAccessor;

            var user = HttpContextAccessor.HttpContext.User;
            var user_name = user.Identity.IsAuthenticated ? $"[{user.Identity.Name}]": null;
            _CartName = $"WebStore.Cart{user_name}"; // если пользователь не залогинился, то название cookies будет WebStore.Cart, в противном случае добавляем в [] его имя
        }

        public void AddToCart(int id) 
        {
            var cart = Cart; // извлекаем корзину из cookies
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id); // пытаемся найти в корзине товар 
            if (item is null)   // если товара не было найдено  в корзине, то надо добавить новый
                cart.Items.Add(new CartItem { ProductId = id, Quantity = 1 });
            else
                item.Quantity++;

            Cart = cart; // корзина обратно сериализуется

        }

        public void DecrementFromCart(int id)
        {
            var cart = Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null)
                return;
            if (item.Quantity > 0)
                item.Quantity--;

            if (item.Quantity == 0)
                cart.Items.Remove(item);

            Cart = cart;
        }

        public void RemoveFromCart(int id) 
        {
            var cart = Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null)
                return;

            cart.Items.Remove(item);

            Cart = cart;
        }

        public void Clear() 
        {
            var cart = Cart;
            cart.Items.Clear();
            Cart = cart;
        }

        public CartViewModel TransformFromCart() 
        {
            var products = _ProductData.GetProducts(new ProductFilter // найти все товары, которые были запрошены в корзине, чтобы извлечь их цену
            {
                Ids = Cart.Items.Select(item => item.ProductId).ToArray() // получаем товары с заданными идентификаторами
            });

            var products_view_models = products.ToView().ToDictionary(p => p.Id); // формируем из них словарь вьюмоделей

            return new CartViewModel
            { 
                Items = Cart.Items.Select(item => (products_view_models[item.ProductId], item.Quantity))
            };
        }

    }
}
