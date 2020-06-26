using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
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

        public IActionResult Index() => View();  // если в () ничего не указано, то ищется представление с именем действия,
                                                 // иначе в () указываем какое конкретно представление хотим отобразить View("Index")


        // адрес, по которому наш сайт выдаст наших сотрудников и отобразит их в виде html-страницы
        // метод, в котором плучаем данные из бизнес-логики, а дальше передадим их на представление 
        // (вызовем метод View и передадим ему модель - перечисление сотрудников)
        public IActionResult Employees()
        {
            ViewBag.Title = "123";// - динамический объект
            ViewData["TestValue"] = "Value - test"; // - словарь

            var employees = _Employees;
            return View(employees);
        } 

        public IActionResult EmployeeInfo(int id)
        {
            var employee = _Employees.FirstOrDefault(e => e.Id == id); // извлекает 1й элемент с каким-то совпадением по какому-то критерию
            if (employee == null)
                return NotFound(); // код ошибки 404

            return View(employee);
        }
    }
}
