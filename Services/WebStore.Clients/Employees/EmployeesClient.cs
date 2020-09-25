using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Employees
{
    public class EmployeesClient : BaseClient, IEmployeesData
    {
        public EmployeesClient(IConfiguration Configuration) : base(Configuration, "api/employees") { }

        public IEnumerable<Employee> Get() => Get<IEnumerable<Employee>>(_ServiceAddress);

        public Employee GetById(int id) => Get<Employee>($"{_ServiceAddress}/{id}");

        public int Add(Employee employee) => Post(_ServiceAddress, employee).Content.ReadAsAsync<int>().Result;

        public void Edit(Employee employee) => Put(_ServiceAddress, employee);

        public bool Delete(int id) => Delete($"{_ServiceAddress}/{id}").IsSuccessStatusCode;

        public void SaveChanges(){}
    }
}
