using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Infrastructure.MiddleWare
{
    public class TestMiddleWare
    {
        private readonly RequestDelegate _Next;
        public TestMiddleWare(RequestDelegate Next) => _Next = Next;
        
        public async Task Invoke(HttpContext Context)
        {
            await _Next(Context);
        }
    }
}
