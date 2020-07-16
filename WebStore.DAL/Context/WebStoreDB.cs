using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities;

namespace WebStore.DAL.Context
{
    public class WebStoreDB: DbContext
    {
        // определяем набор таблиц, с которыми хотим работать
        // консоль диспетчера пакетов:
        // 1. Переориентировать её на WebStoreDB
        // 2. Выполнить Add-Migration Initial
        // 3. Выполнить Update-Database
        public DbSet<Product> Products { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public WebStoreDB(DbContextOptions<WebStoreDB> Options) : base (Options) {}
    }
}
