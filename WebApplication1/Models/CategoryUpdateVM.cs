using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class CategoryUpdateVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(55)]
        public string? CategoryName { get; set; }

    }
}
