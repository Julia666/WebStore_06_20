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
    // [Route("api/[controller]")] // http://localhost:5001/api/EmployeesApi
    // [Route("api/employees")] // http://localhost:5001/api/employees
    [Route(WebAPI.Employees)]
    [Produces("application/json")]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData _EmployeesData;

        public EmployeesApiController(IEmployeesData EmployeesData) => _EmployeesData = EmployeesData;

        [HttpGet] // http://localhost:5001/api/employees
        // [HttpGet("all")] // http://localhost:5001/api/employees
        public IEnumerable<Employee> Get() => _EmployeesData.Get();

        [HttpGet("{id}")] // http://localhost:5001/api/employees/5
        public Employee GetById(int id) => _EmployeesData.GetById(id);

        [HttpPost]
        public int Add(Employee employee)
        {
            var id = _EmployeesData.Add(employee);
            SaveChanges();
            return id;
        }

        [HttpPut]
        public void Edit(Employee employee)
        {
            _EmployeesData.Edit(employee);
            SaveChanges();
        }

        [HttpDelete("{id}")] // http://localhost:5001/api/employees/5
        public bool Delete(int id)
        {
            var result = _EmployeesData.Delete(id);
            SaveChanges();
            return result;
        }

        // [NonAction]
        public void SaveChanges() => _EmployeesData.SaveChanges();
    }
}
