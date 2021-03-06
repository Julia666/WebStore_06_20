﻿using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;

namespace WebStore.Services.Mapping
{
    public static class EmployeeMapper
    {
        public static EmployeesViewModel ToView(this Employee employee) => new EmployeesViewModel
        {
            Id = employee.Id,
            FirstName = employee.Name,
            LastName = employee.SurName,
            Patronymic = employee.Patronymic,
            Age = employee.Age,
            EmployementDate = employee.EmployementDate
        };

        public static IEnumerable<EmployeesViewModel> ToView(this IEnumerable<Employee> employees) => employees.Select(ToView);

        public static Employee FromView(this EmployeesViewModel Model) => new Employee
        {
            Id = Model.Id,
            SurName = Model.LastName,
            Name = Model.FirstName,
            Patronymic = Model.Patronymic,
            Age = Model.Age,
            EmployementDate = Model.EmployementDate
        };
    }
}
