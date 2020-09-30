using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities
{
    /// <summary>Сотрудник</summary>
    public class Employee : NamedEntity
    {
        /// <summary>Фамилия</summary>
        public string SurName { get; set; }

        /// <summary>Отчество</summary>
        public string Patronymic { get; set; }

        /// <summary>Возраст</summary>
        public int Age { get; set; }
        public string FullName
        {
            get { return SurName + " " + Name + " " + Patronymic; }
        }

        /// <summary>Дата поступления на работу</summary>
        public DateTime EmployementDate { get; set; }
    }
}
