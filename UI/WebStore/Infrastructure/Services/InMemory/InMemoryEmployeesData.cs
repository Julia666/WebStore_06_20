using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Data;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services.InMemory
{
    public class InMemoryEmployeesData : IEmployeesData
    {
        private readonly List<Employee> _Employees = TestData.Employees;
        public int Add(Employee employee) 
        {
            if (employee is null) // проверка на пустую ссылку
                throw new ArgumentNullException(nameof(employee));

            if (_Employees.Contains(employee)) // проверка,что объект employee еще не находится внутри нашего списка _Employees
                return employee.Id;
            employee.Id = _Employees.Count == 0 ? 1 : _Employees.Max(e => e.Id) + 1; // назначаем объекту employee идентификатор
            _Employees.Add(employee); // добавить его в список
            return employee.Id; // вернуть идентификатор
        }

        public bool Delete(int id) => _Employees.RemoveAll(e => e.Id == id) > 0;
       

        public void Edit(Employee employee) 
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));
            if (_Employees.Contains(employee)) // если объект employee уже находится в списке просто делаем возврат
                return;

            var db_employee = GetById(employee.Id);// если вдруг объект не находится в списке, то нам необходимо найти объект из списка по его id
            if (db_employee is null)// проверить, что он действительно найден (если не найден, то и редактировать нечего)
                return;
            db_employee.Name = employee.Name;// переносим данные
            db_employee.SurName = employee.SurName;
            db_employee.Patronymic = employee.Patronymic;
            db_employee.Age = employee.Age;
        }

        public IEnumerable<Employee> Get() => _Employees;

        public Employee GetById(int id) => _Employees.FirstOrDefault(e => e.Id == id);

        public void SaveChanges()
        { }
    }
}
