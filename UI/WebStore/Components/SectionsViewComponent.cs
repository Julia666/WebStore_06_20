using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Components
{
    // [ViewComponent(Name = "Section")] - если не хотим наследоваться от ViewComponent
    public class SectionsViewComponent : ViewComponent
    {
        // Визуальные компоненты - логические элементы, которые используются в представлении как почти обычные теги,
        // которые хранят внутри себя некоторую логику, могут обращаться к сервисам и должны отрендерить некоторую визуальную часть

        // При анализе разметки, которая пишется в синтаксисе Razor, можно вставить в нужное место визуальный компонент Sections,
        // в этом случае обработчик разметки, наткнувшись на этот визуальный компонент, вызовет у него метод Invoke 
        // (в котором можно выполнить какую-то логику, после чего необходимо сформировать представление (Views - Shared - Components))

        private readonly IProductData _ProductData;
        public SectionsViewComponent(IProductData productData) => _ProductData = productData;

        public IViewComponentResult Invoke(string SectionId)
        {
            var section_id = int.TryParse(SectionId, out var id) ? id : (int?)null;

            var sections = GetSections(section_id, out var parent_section_id);

            return View(new SelectableSectionsViewModel
            {
                Sections = sections,
                CurrentSectionId = section_id,
                ParentSectionId = parent_section_id
            });

        }
           

        // public async Task<IViewComponentResult> Invoke() => View();

        private IEnumerable<SectionViewModel> GetSections(int? SectionId, out int? ParentSectionId) // извлекает секции из сервиса
        {
            ParentSectionId = null;
            var sections = _ProductData.GetSections().ToArray();
            var parent_sections = sections.Where(s => s.ParentId is null);   // выгружаем все секции из сервиса и извлекаем все родительские секции
            var parent_sections_views = parent_sections // формируем модели представления для родительских секций
                .Select(s => new SectionViewModel
                { 
                    Id = s.Id,
                    Name =  s.Name,
                    Order = s.Order
                })
                .ToList();

            foreach (var parent_section in parent_sections_views) // находим все дочерние секции
            {
                var childs = sections.Where(s => s.ParentId == parent_section.Id);
                foreach (var child_section in childs)
                {
                    if (child_section.Id == SectionId)
                        ParentSectionId = child_section.ParentId;

                    parent_section.ChildSections.Add(
                        new
                            SectionViewModel // добавляем в родительскую секцию в её коллекцию дочерних секций новый вьюмодель
                            {
                                Id = child_section.Id,
                                Name = child_section.Name,
                                Order = child_section.Order,
                                ParentSection = parent_section
                            });
                }

                parent_section.ChildSections.Sort((a, b) => Comparer<double>.Default.Compare(a.Order, b.Order));   // сортировка               
            }

            parent_sections_views.Sort((a, b) => Comparer<double>.Default.Compare(a.Order, b.Order)); // сортировка родительских секций
            return parent_sections_views;
        }
    }
}
