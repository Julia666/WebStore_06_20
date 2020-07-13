using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace WebStore.DAL.Context
{
    public class WebStoreDB: DbContext
    {
        public WebStoreDB(DbContextOptions<WebStoreDB> Options) : base (Options) {}
    }
}
