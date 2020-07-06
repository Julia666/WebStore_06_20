using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewModels
{
    // передача информации в представление или из представления
    // когда нам возвращается из формы. когда мы формируем представление и вызываем его с помощью метода View,
    // мы должны собрать всю необходимую информацию, чтобы это представление смогло сформироваться, упаковать ее в какой-то контейнер
    // вот этим контейнером и является модельпредставления
    public class EmployeesViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public int Age { get; set; }
    }
}
