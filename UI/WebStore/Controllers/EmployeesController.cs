using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Controllers
{
    // [Route("Users")]
    [Authorize] // ограничить доступ к этому контроллеру для всех незарегистрированных пользователей
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _EmployeesData;
        public EmployeesController(IEmployeesData EmployeesData) => _EmployeesData = EmployeesData;
        

        // [Route("All")]
        public IActionResult Index()
        {
            return View(_EmployeesData.Get().ToView()); // получение всех сотрудников
        }

        // [Route("User-{id}")]
        public IActionResult Details(int id) // посмотреть конкретного сотрудника по его id
        {
            var employee = _EmployeesData.GetById(id); // извлекает 1й элемент с каким-то совпадением по какому-то критерию
            if (employee == null)
                return NotFound(); // код ошибки 404

            return View(employee.ToView());
        }

        #region Edit
        [HttpGet]
        [Authorize(Roles = Role.Administrator)] // редактировать смогут только администраторы
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
            return View(employee.ToView()); 
        }

        [HttpPost] // обрабатывает ответ формы
        public IActionResult Edit(EmployeesViewModel Model)
        {
            if (Model is null)
                throw new ArgumentNullException(nameof(Model));

            if (Model.Age < 18 || Model.Age > 75)
                ModelState.AddModelError(nameof(Employee.Age), "Возраст должен быть от 18 до 75 лет"); // информация об ошибке под полем ввода возраста

            if (Model.FirstName == "123" && Model.LastName == "QWE")
                ModelState.AddModelError(string.Empty, "Странный выбор для имени и фамилии"); // информация добавляется в область для всей модели в целом


            if (!ModelState.IsValid) // результаты валидации модели, если в них ошибка, то IsValid-false
                return View(Model); // и тогда модель отправляем обратно в браузер вместе с результатами валидации (ошибки)
       

            if (Model.Id == 0)
                _EmployeesData.Add(Model.FromView());
            else
                _EmployeesData.Edit(Model.FromView());

            _EmployeesData.SaveChanges();

            return RedirectToAction(nameof(Index)); // перенаправляет на метод Index, чтобы отобразить всех сотрудников
        }
        #endregion

        #region Delete
        [Authorize(Roles = Role.Administrator)] // удалять смогут только администраторы
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var employee = _EmployeesData.GetById(id);// извлекаем сотрудника, которого будем удалять

            if (employee is null)
                return NotFound();

            return View(employee.ToView());  // представление удаления, на которое передадим вьюмодель
                                             // передача вьюмодели на представление         
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id) // подтверждение удаления
        {
            _EmployeesData.Delete(id);
            _EmployeesData.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
