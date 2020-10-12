﻿using System.Collections.Generic;
using System.Linq;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;
using WebStore.Services.Data;
using WebStore.Services.Mapping;

namespace WebStore.Services.Products.InMemory
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<SectionDTO> GetSections() => TestData.Sections.Select(s => s.ToDTO());
        public SectionDTO GetSectionById(int id) => throw new System.NotImplementedException();
        

        public IEnumerable<BrandDTO> GetBrands() => TestData.Brands.Select(b => b.ToDTO());
        public BrandDTO GetBrandById(int id) => throw new System.NotImplementedException();
        

        public IEnumerable<ProductDTO> GetProducts(ProductFilter Filter = null)
        {
            var query = TestData.Products;  // берем все товары как перечисления, после чего накладываем фильтры
            if (Filter?.SectionId != null)
                query = query.Where(product => product.SectionId == Filter.SectionId);

            if (Filter?.BrandId != null)
                query = query.Where(product => product.BrandId == Filter.BrandId);

            return query.ToDTO();

        }

        public ProductDTO GetProductById(int id) => TestData.Products.FirstOrDefault(p => p.Id == id).ToDTO();
      
    }
}
