﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Services.Products.InCookies
{
    public class CookiesCartStore : ICartStore
    {
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly string _CartName;
        public Cart Cart
        {
            get
            {
                var context = _HttpContextAccessor.HttpContext;  // берем контекст http-запроса и из него извлекаем cookies
                var cookies = context.Response.Cookies;
                var cart_cookies = context.Request.Cookies[_CartName]; // пытаемся найти саму cookies, которая отвечает за хранение корзины
                if (cart_cookies is null)  // если cookies вообще нет, то создаем новую корзину и сериализуем её
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

        public CookiesCartStore(IHttpContextAccessor HttpContextAccessor)
        {
            _HttpContextAccessor = HttpContextAccessor;

            var user = HttpContextAccessor.HttpContext.User;
            var user_name = user.Identity.IsAuthenticated ? $"[{user.Identity.Name}]" : null;
            _CartName = $"WebStore.Cart{user_name}";
        }
    }
}
