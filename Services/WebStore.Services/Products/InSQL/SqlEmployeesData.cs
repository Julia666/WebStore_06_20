using System;
using System.Collections.Generic;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Services.Products.InSQL
{
    public class SqlEmployeesData : IEmployeesData
    {
        private readonly WebStoreDB _db;

        public SqlEmployeesData(WebStoreDB db) => _db = db;


        public int Add(Employee employee)
        {
            if (employee is null)
                throw new ArgumentException(nameof(employee));

            _db.Add(employee);
            //_db.Employees.Add(employee);
            return employee.Id;
        }

        public bool Delete(int id)
        {
            var employee = GetById(id);
                if (employee is null)
                    return false;
            _db.Remove(employee);
            //_db.Employees.Remove(employee);
            return true;
        }

        public void Edit(Employee employee)
        {
            if (employee is null)
                throw new ArgumentException(nameof(employee));
            _db.Update(employee);
            //_db.Employees.Update(employee);

        }
        public IEnumerable<Employee> Get() => _db.Employees;

        public Employee GetById(int id) => _db.Employees.Find(id);


        public void SaveChanges() => _db.SaveChanges();

    }
}
