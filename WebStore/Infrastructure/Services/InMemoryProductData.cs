﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Infrastructure.Interfaces;
using WebStoreDomain.Entities;

namespace WebStore.Infrastructure.Services
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Section> GetSections() => TestData.Sections;
        public IEnumerable<Brand> GetBrands() => TestData.Brands;
    }
}
