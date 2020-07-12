using System;
using System.Collections.Generic;
using System.Text;

namespace WebStoreDomain
{
    public class ProductFilter // возможность выдачи не всех товаров, а только удовлетворяющих критерию поиска 
    {
        public int? SectionId { get; set; } // задаем номер секции и номер бренда опционально- 
                                            // если указан номер секции, то должны быть выгружены все товары, у которых есть этот номер
        public int? BrandId { get; set; } 
    }
}
