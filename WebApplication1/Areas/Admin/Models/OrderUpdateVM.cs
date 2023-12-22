using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Areas.Admin.Models
{
    public class OrderUpdateVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public List<SelectListItem>? Products { get; set; }
        public List<SelectListItem>? Customers { get; set; }
    }
}
