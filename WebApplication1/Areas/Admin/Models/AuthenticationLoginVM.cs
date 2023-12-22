using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Areas.Admin.Models
{
    public class AuthenticationLoginVM
    {
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is wrong.")]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password, ErrorMessage = "Password is wrong")]
        public string? Password { get; set; }
    }
}
