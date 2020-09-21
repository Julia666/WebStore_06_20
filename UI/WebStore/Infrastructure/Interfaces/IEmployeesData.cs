using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;

namespace WebStore.Infrastructure.Interfaces
{
    // Процесс разработки логики приложения всегда должен начинаться с интерфейса этой самой логики
    public interface IEmployeesData
    {
        // описываем,что должен уметь делать с сотрудниками наш сервис
        IEnumerable<Employee> Get(); // получить всех сотрудников

        Employee GetById(int id); // получить сотрудника по идентификатору

        int Add(Employee employee); // добавить нового сотрудника (возвращает приписанный ему идентификатор)

        void Edit(Employee employee); // редактировать сотрудника (информация для редактирования 
                                          // должна передаваться сервису в виде этой же модели, мы указываем новый объект
                                          // employee с тем же самым id, в который хотим внести изменения и заполняем все поля,которые нам необходимы для редактирования

        bool Delete(int id);

        void SaveChanges(); // сохранение внесенных изменений в БД
    }
    
}
