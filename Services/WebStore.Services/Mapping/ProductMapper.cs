using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;

namespace WebStore.Services.Mapping
{
    public static class ProductMapper
    {
        public static ProductViewModel ToView(this Product product) => new ProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Order = product.Order,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            Brand = product.Brand?.Name
        };

        public static IEnumerable<ProductViewModel> ToView(this IEnumerable<Product> products) => products.Select(ToView);

        public static ProductDTO ToDTO(this Product product) => product is null ? null : new ProductDTO
        {
            Id = product.Id,
            Name = product.Name,
            Order = product.Order,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            Brand = product.Brand.ToDTO(),
            Section = product.Section.ToDTO(),
        };

        public static IEnumerable<ProductDTO> ToDTO(this IEnumerable<Product> products) => products.Select(ToDTO);

        public static Product FromDTO(this ProductDTO product) => product is null ? null : new Product
        {
            Id = product.Id,
            Name = product.Name,
            Order = product.Order,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            Brand = product.Brand.FromDTO(),
            Section = product.Section.FromDTO(),
        };

        public static IEnumerable<Product> FromDTO(this IEnumerable<ProductDTO> products) => products.Select(FromDTO);
    }
}
