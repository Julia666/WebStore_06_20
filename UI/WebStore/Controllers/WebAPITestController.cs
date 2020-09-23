using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces.TestApi;

namespace WebStore.Controllers
{
    public class WebAPITestController : Controller
    {
        private readonly IValueService _ValueService;

        public WebAPITestController(IValueService ValueService) => _ValueService = ValueService;

        public IActionResult Index()
        {
            var values = _ValueService.Get();
            return View(values);
        }
    }
}
