namespace Entities.Dtos
{
    public class ProductDto
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
