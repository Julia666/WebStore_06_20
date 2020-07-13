using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WebStoreDomain.Entities;

namespace WebStore.DAL.Context
{
    public class WebStoreDB: DbContext
    {
        // определяем набор таблиц, с которыми хотим работать
        public DbSet<Product> Products { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public WebStoreDB(DbContextOptions<WebStoreDB> Options) : base (Options) {}
    }
}
