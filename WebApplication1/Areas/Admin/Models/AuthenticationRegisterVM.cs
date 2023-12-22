using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Areas.Admin.Models
{
    public class AuthenticationRegisterVM
    {
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string? FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string? LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is wrong.")]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password, ErrorMessage = "Password is wrong")]
        public string? Password { get; set; }
    }
}
