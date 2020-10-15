using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Domain.ViewModels.Identity
{
    public class RegisterUserViewModel
    {
        // что пользователь должен заполнить, чтобы зарегистрироваться
        [Required(ErrorMessage = "Обязательное поле")] 
        [MaxLength(256, ErrorMessage = "Длина строки должна быть меньше 256 символов")]
        [MinLength(4, ErrorMessage = "Длина строки должна быть больше 4 символов")]
        [Display(Name = "Имя пользователя")]
        [Remote("IsNameFree", "Account")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Подтверждение пароля")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Пароли должны совпадать")]
        public string PasswordConfirm { get; set; }

    }
}
