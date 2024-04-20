using System.ComponentModel.DataAnnotations;

namespace JakubWiesniakLab3.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Nazwa uzytkownika jest wymagana")]
        [Display(Name = "Login")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Haslo jest wymagana")]
        [Display(Name = "Haslo")]
        public required string Password { get; set; }
    }
}