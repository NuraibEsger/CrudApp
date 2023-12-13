using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string? ImageName { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
