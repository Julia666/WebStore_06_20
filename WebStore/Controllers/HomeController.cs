using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
      

        public IActionResult Index() => View();  // если в () ничего не указано, то ищется представление с именем действия,
                                                 // иначе в () указываем какое конкретно представление хотим отобразить View("Index")

        public IActionResult Throw(string id) => 
            throw new ApplicationException($"Исключение: {id ?? "<null>"}");

        public IActionResult Blogs() => View();
        public IActionResult BlogSingle() => View();   
        public IActionResult Cart() => View();
        public IActionResult Checkout() => View();
        public IActionResult ContactUs() => View();
        public IActionResult ProductDetails() => View();
        public IActionResult Error404() => View();

        [ActionName("Content")]
        public IActionResult GetContent(string Id) => Content($"Content: {Id}"); // будет возвращать тот текст, который мы отправим
    }
}
