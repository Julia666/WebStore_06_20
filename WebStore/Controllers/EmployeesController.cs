using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Data;
using WebStore.Models;

namespace WebStore.Controllers
{
    // [Route("Users")]
    public class EmployeesController : Controller
    {
       

        // [Route("All")]
        public IActionResult Index()
        {
            return View(TestData.Employees);
        }

        // [Route("User-{id}")]
        public IActionResult Details(int id)
        {
            var employee = TestData.Employees.FirstOrDefault(e => e.Id == id); // извлекает 1й элемент с каким-то совпадением по какому-то критерию
            if (employee == null)
                return NotFound(); // код ошибки 404

            return View(employee);
        }
    }

}
