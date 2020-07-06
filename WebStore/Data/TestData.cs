using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Data
{
    public static class TestData
    {
        public static List<Employee> Employees { get; } = new List<Employee>
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
    }
}
