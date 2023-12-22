using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Entities;

namespace WebApplication1.Areas.Admin.Models
{
    public class CategoryAddVM
    {
        [Required]
        [MinLength(2)]
        [MaxLength(55)]
        public string? CategoryName { get; set; }
    }
}
