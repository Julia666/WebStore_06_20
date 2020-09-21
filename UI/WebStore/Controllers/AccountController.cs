using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebStore.Domain.Entities.Identity;
using WebStore.ViewModels.Identity;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;

        public AccountController(UserManager<User> UserManager, SignInManager<User> SignInManager)
        {
            _UserManager = UserManager;
            _SignInManager = SignInManager;
        }

        #region Процесс регистрации нового пользователя
        public IActionResult Register() => View(new RegisterUserViewModel()); // отправляет пустую регистрационную форму

        [HttpPost, ValidateAntiForgeryToken] // если вдруг сервер отправит один токен, а в ответ ему будет прислан другой токен (либо не прислан вообще), то сервер отменит это действие в принципе
        public async Task<IActionResult> Register(RegisterUserViewModel Model) // принимает эту форму обратно
        {
            if (!ModelState.IsValid) // валидация формы, если что-то не так, то пользователю модель отправляется на доработку
                return View(Model);

            var user = new User  // формируем нового пользователя снужным именем
            {
                UserName = Model.UserName
            };

            var registration_result = await _UserManager.CreateAsync(user, Model.Password); // получаем объект с результатами процесса регистрации
            if(registration_result.Succeeded) // если процесс регистрации прошел успешно
            {
                await _UserManager.AddToRoleAsync(user, Role.User);

                await _SignInManager.SignInAsync(user, false); // просим _SignInManager войти этим пользователем в систему и не запоминать процесс входа(в след.раз вводить пароль заново)
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in registration_result.Errors)  // если что-то пошло не так, надо взять все ошибки регистрации которые есть
                ModelState.AddModelError(string.Empty, error.Description); // взять каждую ошибку и добавить ее в ModelState
            return View(Model);
        }
        #endregion

        #region Процесс входа пользователя в систему
        public IActionResult Login(string ReturnUrl) => View(new LoginViewModel { ReturnUrl = ReturnUrl });

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel Model)
        {
            if (!ModelState.IsValid) 
                return View(Model);

            var login_result = await _SignInManager.PasswordSignInAsync(
                Model.UserName,
                Model.Password,
                Model.RememberMe,
                false); // блокировать или не блокировать в случае неудачи (блокировать)

            if(login_result.Succeeded)
            {
                if (Url.IsLocalUrl(Model.ReturnUrl)) // защита от хакеров
                    return Redirect(Model.ReturnUrl);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Неверное имя пользователя или пароль!");
            return View(Model);
        }
        #endregion
        public async Task<IActionResult> Logout()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home"); 
        }

        public IActionResult AccessDenied() => View();
    }
}
