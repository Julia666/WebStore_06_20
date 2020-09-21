using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Domain.ViewModels
{
    // передача информации в представление или из представления
    // когда нам возвращается из формы. когда мы формируем представление и вызываем его с помощью метода View,
    // мы должны собрать всю необходимую информацию, чтобы это представление смогло сформироваться, упаковать ее в какой-то контейнер
    // вот этим контейнером и является модельпредставления
    public class EmployeesViewModel  // : IValidatableObject
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Имя является обязательным")] // обязательный параметр
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Количество символов в имени должно быть от 3 до 200")] // допустимый размер строки
        [RegularExpression(@"([А-ЯЁ][а-яё]+)|([A - Z][a - z]+)", ErrorMessage = "Ошибка формата имени")] // регулярное выражение,которому должна соответствовать строка
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Фамилия является обязательным")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Количество символов в фамилии должно быть от 3 до 200")]
        public string LastName { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Display(Name = "Возраст")]
        [Required(ErrorMessage = "Возраст является обязательным")]
        [Range(20, 80, ErrorMessage = "Возраст должен быть в пределах от 20 до 80 лет")]
        public int Age { get; set; }

        [Display(Name = "Дата начала трудового договора")]
        [DataType(DataType.DateTime)]
        public DateTime EmployementDate { get; set; }
    }
}
