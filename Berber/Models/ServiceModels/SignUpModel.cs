using System.ComponentModel.DataAnnotations;

namespace Berber.Models.ServiceModels
{
    public class SignUpModel
    {
        [Required]
        [Display(Name = "İsim Soyisim")]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Parolalar uyuşmuyor")]
        [Display(Name = "Tekrar Şifre")]
        public string ConfirmPassword { get; set; }
        [Phone]
        [Display(Name = "Telefon Numarası")]
        public string PhoneNumber { get; set; }
    }
}
