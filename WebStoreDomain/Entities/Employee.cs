using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
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
