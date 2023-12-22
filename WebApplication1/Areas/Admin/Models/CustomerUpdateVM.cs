using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Areas.Admin.Models
{
    public class CustomerUpdateVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(55)]
        public string? Name { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(55)]
        public string? Surname { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
