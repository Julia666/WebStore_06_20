using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Services.Data
{
    // для первичной инициализации БД после её создания, сформируем сервис инициализации БД
    public class WebStoreDBInitializer
    {
        private readonly WebStoreDB _db;
        private readonly UserManager<User> _UserManager;
        private readonly RoleManager<Role> _RoleManager;
        private readonly ILogger<WebStoreDBInitializer> _Logger;

        public WebStoreDBInitializer(WebStoreDB db, UserManager<User> UserManager, RoleManager<Role> RoleManager, ILogger<WebStoreDBInitializer> Logger)  // в конструкторе просим выдать базу данных WebStoreDB
        {
            _db = db;
            _UserManager = UserManager;
            _RoleManager = RoleManager;
            _Logger = Logger;
        }
        public void Initialize()
        {
            _Logger.LogInformation("Инициализация БД...");

            var db = _db.Database;

            // if (db.EnsureDeleted()) //  удаление
            //    if (!db.EnsureCreated()) // создание заново
            //        throw new InvalidOperationException("Ошибка при создании БД");

            try
            {
                _Logger.LogInformation("Проведение миграций БД");
                db.Migrate(); // создает БД, если её не было

                _Logger.LogInformation("Инициализация каталога товаров");
                InitializeProducts();

                _Logger.LogInformation("Инициализация сотрудников");
                InitializeEmployees();

                _Logger.LogInformation("Инициализация данных системы Identity");
                InitializeIdentityAsync().Wait();
            }
            catch (Exception error)
            {
                _Logger.LogCritical(new EventId(0), error, "Ошибка процесса инициализации базы данных");

                //throw;
            }

            _Logger.LogInformation("Инициализация БД выполнена успешно");
        }

        private void InitializeProducts()
        {

            if (_db.Products.Any()) // если в контексте есть хотя бы один товар
            {
                _Logger.LogInformation("Каталог товаров уже инециализирован");
                return; // это значит, что БД уже проинициализирована и дальнейшая работа инициализатора не требуется
            }
        

        var db = _db.Database;
            // если товаров нет, то заполняем БД сперва секциями, потом брендами, потом товарами

            using (db.BeginTransaction())    // с помощью транзакций - можем добавить либо все секции вместе, либо не добавлять ни одной. 
            {                                //Если хотя бы одна секция не сможет быть добавлена, значит вся операция откатится обратно
                _db.Sections.AddRange(TestData.Sections);     // добавляем все секции

                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[ProductSections] ON"); // отключаем первичный ключ (БД не любит жестко заданный)

                _db.SaveChanges();

                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[ProductSections] OFF");

                db.CommitTransaction();
            }

            using (db.BeginTransaction())
            {
                _db.Brands.AddRange(TestData.Brands);

                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[ProductBrands] ON");

                _db.SaveChanges();

                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[ProductBrands] OFF");

                db.CommitTransaction();
            }

            using (db.BeginTransaction())
            {
                _db.Products.AddRange(TestData.Products);

                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] ON");

                _db.SaveChanges();

                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] OFF");

                db.CommitTransaction();
            }




            /*
            var products = TestData.Products;
            var sections = TestData.Sections;
            var brands = TestData.Brands;

            var product_section = products.Join(   // указываем,что хотим объединиться по двум ключам: у товара внешний ключ-SectionId, а у секции первичный ключ-Id
                sections, 
                p => p.SectionId,
                s => s.Id, (product, section) => (product, section));

            foreach(var (product, section) in product_section)
            {
                product.Section = section;
                product.SectionId = 0;    // сбрасываем внешний ключ
            }

            var product_brand = products.Join(   
              brands,
              p => p.BrandId,
              b => b.Id, (product, brand) => (product, brand));

            foreach (var (product, brand) in product_brand)
            {
                product.Brand = brand;
                product.BrandId = null;    
            }

            foreach (var product in products)
                product.Id = 0;

            var child_section = sections.Join(sections,
                child => child.ParentId,
                parent => parent.Id,
                (child, parent) => (child, parent));

            foreach(var(child, parent) in child_section)
            {
                child.ParentSection = parent;
                child.ParentId = null;
            }

            foreach (var section in sections)
                section.Id = 0;

            foreach (var brand in brands)
                brand.Id = 0;

            using (db.BeginTransaction())
            {
                _db.Sections.AddRange(sections);
                _db.Brands.AddRange(brands);
                _db.Products.AddRange(products);
                _db.SaveChanges();
                db.CommitTransaction();
            }
            */
        }

        private void InitializeEmployees()
        {
            if (_db.Employees.Any())
            {
                _Logger.LogInformation("Раздел сотрудников уже инициализирован");
                return;
            }
            using (_db.Database.BeginTransaction())
            {
                TestData.Employees.ForEach(employee => employee.Id = 0);

                _db.Employees.AddRange(TestData.Employees);

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }
        }

        private async Task InitializeIdentityAsync()
        {
            async Task CheckRoleExist(string RoleName) // контролирует наличие роли с указанным именем
            {
                if (!await _RoleManager.RoleExistsAsync(RoleName))
                {
                    _Logger.LogInformation("Добавление роли пользователя {0}", RoleName);
                    await _RoleManager.CreateAsync(new Role { Name = RoleName });
                }
            }

            await CheckRoleExist(Role.Administrator);
            await CheckRoleExist(Role.User);

            if (await _UserManager.FindByNameAsync(User.Administrator) is null) // проверка наличия самого пользователя с именем Администратор
            {
                var admin = new User { UserName = User.Administrator };
                var creation_result = await _UserManager.CreateAsync(admin, User.DefaultAdminPassword);
                if (creation_result.Succeeded)
                {
                    _Logger.LogInformation("Пользователь {0} добавлен", User.Administrator);
                    await _UserManager.AddToRoleAsync(admin, Role.Administrator);
                    _Logger.LogInformation("Пользователю {0} добавлена роль {1}", User.Administrator,Role.Administrator);
                }
                else
                {
                    var errors = creation_result.Errors.Select(e => e.Description);
                    throw new InvalidOperationException($"Ошибка при создании пользователя Администратор: {string.Join(", ", errors)}");
                }
            }
        }
    }
}
