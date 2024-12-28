using System.ComponentModel.DataAnnotations;

namespace Berber.Models.ServiceModels
{
    public class SignInModel
    {
        [Required]
        [EmailAddress]
        [Display(Name ="Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
    }
}
