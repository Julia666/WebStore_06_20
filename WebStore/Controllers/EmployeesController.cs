using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    // [Route("Users")]
    public class EmployeesController : Controller
    {
        private static readonly List<Employee> _Employees = new List<Employee>
        {
            new Employee
            {
                Id = 1,
                SurName = "Иванов",
                Name = "Иван",
                Patronymic = "Иванович",
                Age = 39
            },

            new Employee
            {
                Id = 2,
                SurName = "Петров",
                Name = "Пётр",
                Patronymic = "Петрович",
                Age = 27
            },

            new Employee
            {
                Id = 3,
                SurName = "Сидоров",
                Name = "Сидор",
                Patronymic = "Сидорович",
                Age = 18
            },
        };

        // [Route("All")]
        public IActionResult Index()
        {
            return View(_Employees);
        }

        // [Route("User-{id}")]
        public IActionResult Details(int id)
        {
            var employee = _Employees.FirstOrDefault(e => e.Id == id); // извлекает 1й элемент с каким-то совпадением по какому-то критерию
            if (employee == null)
                return NotFound(); // код ошибки 404

            return View(employee);
        }
    }

}
