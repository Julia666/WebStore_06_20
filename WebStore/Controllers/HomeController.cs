using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(); // если в () ничего не указано, то ищется представление с именем действия, 
                            // иначе в () указываем какое конкретно представление хотим отобразить View("Index")
        }
    }
}
