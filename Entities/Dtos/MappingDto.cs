using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using AutoMapper;

namespace Entities.Dtos
{
    public class MappingDto
    {
        public ProductCategory productCategories { get; set; }
        public List<Product> product { get; set; }
        public List<ProductCategory> productCategoryList { get; set; }

    }
}
