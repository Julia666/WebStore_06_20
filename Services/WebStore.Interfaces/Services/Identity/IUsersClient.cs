using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Interfaces.Services.Identity
{
    public interface IUsersClient: 
        IUserRoleStore<User>, // позволяет наделять пользователя ролями
        IUserPasswordStore<User>, // хранилище паролей 
        IUserEmailStore<User>,
        IUserPhoneNumberStore<User>,
        IUserTwoFactorStore<User>, // хранилище фактов активации двухфакторной авторизации
        IUserClaimStore<User>,
        IUserLoginStore<User> // хранилище фактов входа пользователя в систему
    {
    }
}
