using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;

namespace WebStore.Services.Mapping
{
    public static class SectionMapper
    {
        public static SectionDTO ToDTO(this Section section) => section is null ? null : new SectionDTO
        {
            Id = section.Id,
            Name = section.Name,
            Order = section.Order,
            ParentId = section.ParentId,
        };

        public static IEnumerable<SectionDTO> ToDTO(this IEnumerable<Section> sections) => sections.Select(ToDTO);

        public static Section FromDTO(this SectionDTO section) => section is null ? null : new Section
        {
            Id = section.Id,
            Name = section.Name,
            ParentId = section.ParentId,
        };
    }
}
