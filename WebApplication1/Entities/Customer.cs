using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
