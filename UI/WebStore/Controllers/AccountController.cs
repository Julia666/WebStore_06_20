using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.ViewModels.Identity;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly ILogger<AccountController> _Logger;

        public AccountController(UserManager<User> UserManager, SignInManager<User> SignInManager, ILogger<AccountController> Logger)
        {
            _UserManager = UserManager;
            _SignInManager = SignInManager;
            _Logger = Logger;
        }

        #region Процесс регистрации нового пользователя
        public IActionResult Register() => View(new RegisterUserViewModel()); // отправляет пустую регистрационную форму

        [HttpPost, ValidateAntiForgeryToken] // если вдруг сервер отправит один токен, а в ответ ему будет прислан другой токен (либо не прислан вообще), то сервер отменит это действие в принципе
        public async Task<IActionResult> Register(RegisterUserViewModel Model) // принимает эту форму обратно
        {
            if (!ModelState.IsValid) // валидация формы, если что-то не так, то пользователю модель отправляется на доработку
                return View(Model);

            using (_Logger.BeginScope("Регистрация пользователя {0}", Model.UserName))
            {
                _Logger.LogInformation("Начало процесса регистрации нового пользователя {0}", Model.UserName);

                var user = new User // формируем нового пользователя снужным именем
                {
                    UserName = Model.UserName
                };

                var registration_result =
                    await _UserManager.CreateAsync(user,
                        Model.Password); // получаем объект с результатами процесса регистрации
                if (registration_result.Succeeded) // если процесс регистрации прошел успешно
                {
                    _Logger.LogInformation("Пользователь {0} успешно зарегистрирован", user.UserName);

                    await _UserManager.AddToRoleAsync(user, Role.User);

                    _Logger.LogInformation("Пользователь {0} наделён ролью {1}", user.UserName, Role.User);

                    await _SignInManager.SignInAsync(user,
                        false); // просим _SignInManager войти этим пользователем в систему и не запоминать процесс входа(в след.раз вводить пароль заново)

                    _Logger.LogInformation("Пользователь {0} автоматически вошёл в систему после регистрации",
                        user.UserName);

                    return RedirectToAction("Index", "Home");
                }

                _Logger.LogWarning("Ошибка при регистрации нового пользователя {0}\r\n",
                    Model.UserName,
                    string.Join(Environment.NewLine, registration_result.Errors.Select(error => error.Description)));

                foreach (var error in registration_result.Errors
                ) // если что-то пошло не так, надо взять все ошибки регистрации которые есть
                    ModelState.AddModelError(string.Empty,
                        error.Description); // взять каждую ошибку и добавить ее в ModelState
            }

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

            _Logger.LogInformation("Попытка входа пользователя {0} в систему", Model.UserName);

            if(login_result.Succeeded)
            {
                _Logger.LogInformation("Пользователь {0} успешно вошёл в систему", Model.UserName);

                if (Url.IsLocalUrl(Model.ReturnUrl)) // защита от хакеров
                    return Redirect(Model.ReturnUrl);
                return RedirectToAction("Index", "Home");
            }

            _Logger.LogWarning("Ошибка имени пользователя или пароля при попытке входа {0}", Model.UserName);

            ModelState.AddModelError(string.Empty, "Неверное имя пользователя или пароль!");
            return View(Model);
        }
        #endregion
        public async Task<IActionResult> Logout()
        {
            var user_name = User.Identity.Name;
            await _SignInManager.SignOutAsync();
            _Logger.LogInformation("Пользователь {0} вышел из системы", user_name);
            return RedirectToAction("Index", "Home"); 
        }

        public IActionResult AccessDenied() => View();
    }
}
