using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Data;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    // [Route("Users")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _EmployeesData;
        public EmployeesController(IEmployeesData EmployeesData) => _EmployeesData = EmployeesData;
        

        // [Route("All")]
        public IActionResult Index()
        {
            return View(_EmployeesData.Get()); // получение всех сотрудников
        }

        // [Route("User-{id}")]
        public IActionResult Details(int id) // посмотреть конкретного сотрудника по его id
        {
            var employee = _EmployeesData.GetById(id); // извлекает 1й элемент с каким-то совпадением по какому-то критерию
            if (employee == null)
                return NotFound(); // код ошибки 404

            return View(employee);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id is null) // если идентификатор не передан
              return View(new EmployeesViewModel()); // то создаем нового сотрудника и передаем пустую модель на представление

            if (id < 0)
                return BadRequest();

            var employee = _EmployeesData.GetById((int)id); // извлекаем из сервиса редактируемого сотрудника
            if (employee is null) //если сервис не нашел сотрудника
                return NotFound();
            // если сотрудник найден, создаем вьюмодель и пердаем на нее информацию
            return View(new EmployeesViewModel
            {
                Id = employee.Id, // передача вьюмодели на представление
                FirstName = employee.Name,
                LastName = employee.SurName,
                Patronymic = employee.Patronymic,
                Age = employee.Age

            }); ; ; 
        }

        [HttpPost] // обрабатывает ответ формы
        public IActionResult Edit(EmployeesViewModel Model)
        {
            return RedirectToAction(nameof(Index)); // перенаправляет на метод Index, чтобы отобразить всех сотрудников
        }
    }

}
