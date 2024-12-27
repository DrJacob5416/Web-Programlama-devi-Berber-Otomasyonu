using System.ComponentModel.DataAnnotations;

namespace Berber.Models.ServiceModels
{
    public class SignUpModel
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Parolalar uyuşmuyor")]
        public string ConfirmPassword { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
