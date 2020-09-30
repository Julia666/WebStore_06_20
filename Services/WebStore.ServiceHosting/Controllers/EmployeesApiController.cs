using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    /// <summary>API управления сотрудниками</summary>
    // [Route("api/[controller]")] // http://localhost:5001/api/EmployeesApi
    // [Route("api/employees")] // http://localhost:5001/api/employees
    [Route(WebAPI.Employees)]
    [Produces("application/json")]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData _EmployeesData;

        public EmployeesApiController(IEmployeesData EmployeesData) => _EmployeesData = EmployeesData;

        /// <summary>
        /// Получить всех доступных сотрудников
        /// </summary>
        /// <returns></returns>
        [HttpGet] // http://localhost:5001/api/employees
        // [HttpGet("all")] // http://localhost:5001/api/employees
        public IEnumerable<Employee> Get() => _EmployeesData.Get();

        /// <summary>Найти сотрудника по идентификатору</summary>
        /// <param name="id">Идентификатор искомого сотрудника</param>
        /// <returns>Найденный сотрудник</returns>
        [HttpGet("{id}")] // http://localhost:5001/api/employees/5
        public Employee GetById(int id) => _EmployeesData.GetById(id);

        /// <summary>Добавление нового сотрудника</summary>
        /// <param name="employee">Новый сотрудник</param>
        /// <returns>Идентификатор добавленного сорудника</returns>
        [HttpPost]
        public int Add(Employee employee)
        {
            var id = _EmployeesData.Add(employee);
            SaveChanges();
            return id;
        }

        /// <summary>Редактирование</summary>
        /// <param name="employee"></param>
        [HttpPut]
        public void Edit(Employee employee)
        {
            _EmployeesData.Edit(employee);
            SaveChanges();
        }

        /// <summary>Удаление</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")] // http://localhost:5001/api/employees/5
        public bool Delete(int id)
        {
            var result = _EmployeesData.Delete(id);
            SaveChanges();
            return result;
        }

        [NonAction]
        public void SaveChanges() => _EmployeesData.SaveChanges();
    }
}
