using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Domain.DTO.Identity
{
    public abstract class UserDTO
    {
        //  Сущность пользователя. Все остальные DTO-модели для реализации наших собств.интерфейсов будут пользоваться наследниками этого
        // класса и внутри себя содержать информацию  о пользователе, для которого будет передаваться информация
        public User User { get; set; } 
    }

    public class AddLoginDTO : UserDTO
    {
        // Для операции добавления факта входа в систему.
        public UserLoginInfo UserLoginInfo { get; set; } // содержит инфо о времени входа в систему, провайдере, параметрах доступа и название
    }

    public class PasswordHashDTO : UserDTO
    {
        public string Hash { get; set; } // hash-код пароля пользователя
    }

    public class SetLockoutDTO : UserDTO
    {
        public  DateTimeOffset? LockoutEnd { get; set; } // время окончания блокировки (например, всех новых пользователей или после неверных попыток ввода паролей)
    }

}
