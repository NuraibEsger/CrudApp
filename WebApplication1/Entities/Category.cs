using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string? CategoryName { get; set; }
        public List<Product>? Products { get; set; }
    }
}
