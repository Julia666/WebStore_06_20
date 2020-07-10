﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;
using WebStoreDomain.Entities;

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
                Age = 39,
                EmployementDate = DateTime.Now.Subtract(TimeSpan.FromDays(300*7))
            },

            new Employee
            {
                Id = 2,
                SurName = "Петров",
                Name = "Пётр",
                Patronymic = "Петрович",
                Age = 27,
                EmployementDate = DateTime.Now.Subtract(TimeSpan.FromDays(512*3))
            },

            new Employee
            {
                Id = 3,
                SurName = "Сидоров",
                Name = "Сидор",
                Patronymic = "Сидорович",
                Age = 18,
                EmployementDate = DateTime.Now.Subtract(TimeSpan.FromDays(200*1))
            },
        };

        public static IEnumerable<Section> Sections { get; } = new[]
        {
            new Section { Id = 1, Name = "Спорт", Order = 0},
            new Section { Id = 2, Name = "Nike", Order = 0, ParentId = 1},
            new Section { Id = 3, Name = "Under Armour", Order = 1, ParentId = 1},
            new Section { Id = 4, Name = "Adidas", Order = 2, ParentId = 1},
            new Section { Id = 5, Name = "Puma", Order = 3, ParentId = 1},
            new Section { Id = 6, Name = "ASICS", Order = 4, ParentId = 1},
            new Section { Id = 7, Name = "Для мужчин", Order = 1},
            new Section { Id = 8, Name = "Fendi", Order = 0, ParentId = 7},
            new Section { Id = 9, Name = "Guess", Order = 1, ParentId = 7},
            new Section { Id = 10, Name = "Valentino", Order = 2, ParentId = 7},
            new Section { Id = 11, Name = "Диор", Order = 3, ParentId = 7},
            new Section { Id = 12, Name = "Версачи", Order = 4, ParentId = 7},
            new Section { Id = 13, Name = "Армани", Order = 5, ParentId = 7},
            new Section { Id = 14, Name = "Prada", Order = 6, ParentId = 7},
            new Section { Id = 15, Name = "Дольче и Габанна", Order = 7, ParentId = 7},
            new Section { Id = 16, Name = "Шанель", Order = 8, ParentId = 7},
            new Section { Id = 17, Name = "Гуччи", Order = 9, ParentId = 7},
            new Section { Id = 18, Name = "Для женщин", Order = 2},
            new Section { Id = 19, Name = "Fendi", Order = 0, ParentId = 18},
            new Section { Id = 20, Name = "Guess", Order = 1, ParentId = 18},
            new Section { Id = 21, Name = "Valentino", Order = 2, ParentId = 18},
            new Section { Id = 22, Name = "Dior", Order = 3, ParentId = 18},
            new Section { Id = 23, Name = "Versace", Order = 4, ParentId = 18},
            new Section { Id = 24, Name = "Для детей", Order = 3},
            new Section { Id = 25, Name = "Мода", Order = 4},
            new Section { Id = 26, Name = "Для дома", Order = 5},
            new Section { Id = 27, Name = "Интерьер", Order = 6},
            new Section { Id = 28, Name = "Одежда", Order = 7},
            new Section { Id = 29, Name = "Сумки", Order = 8},
            new Section { Id = 30, Name = "Обувь", Order = 9},
        };

        public static IEnumerable<Brand> Brands { get; } = new[]
        {
            new Brand { Id = 1, Name = "Acne", Order = 0},
            new Brand { Id = 2, Name = "Grune Erde", Order = 1},
            new Brand { Id = 3, Name = "Abiro", Order = 2},
            new Brand { Id = 4, Name = "Ronhill", Order = 3},
            new Brand { Id = 5, Name = "Oddmolly", Order = 4},
            new Brand { Id = 6, Name = "Boudestijn", Order = 5},
            new Brand { Id = 7, Name = "Rosh creative culture", Order = 6}
        };
    }
}
