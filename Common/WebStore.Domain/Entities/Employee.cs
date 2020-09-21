using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities
{
    public class Employee : NamedEntity
    {
        public string SurName { get; set; }
        public string Patronymic { get; set; }
        public int Age { get; set; }
        public string FullName
        {
            get { return SurName + " " + Name + " " + Patronymic; }
        }
        public DateTime EmployementDate { get; set; }
    }
}
