using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.Concrete
{
    public class Product : IEntity 
    {
        public int Id { get; set; }
        public int ProductCatalogId { get; set; }
        public string Name { get; set; }
        public short UnitsOnOrder { get; set; }
        public short UnitsInStock { get; set; }
        public short QuantityPerUnit { get; set; }
        public decimal Price { get; set; }
    }
}
