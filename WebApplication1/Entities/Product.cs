using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public ProductImage? ProductImage { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
