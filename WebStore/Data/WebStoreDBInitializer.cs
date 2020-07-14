using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;

namespace WebStore.Data
{
    // для первичной инициализации БД после её создания, сформируем сервис инициализации БД
    public class WebStoreDBInitializer
    {
        private readonly WebStoreDB _db;

        public WebStoreDBInitializer(WebStoreDB db) => _db = db; // в конструкторе просим выдать базу данных WebStoreDB

        public void Initialize()
        {
            var db = _db.Database;

            // if (db.EnsureDeleted()) //  удаление
            //    if (!db.EnsureCreated()) // создание заново
            //        throw new InvalidOperationException("Ошибка при создании БД");

            db.Migrate(); // создает БД, если её не было

            if (_db.Products.Any()) // если в контексте есть хотя бы один товар
                return; // это значит, что БД уже проинициализирована и дальнейшая работа инициализатора не требуется

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
        }

    }
}
