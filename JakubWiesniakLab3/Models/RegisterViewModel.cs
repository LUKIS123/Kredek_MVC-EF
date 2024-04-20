using System.ComponentModel.DataAnnotations;

namespace JakubWiesniakLab3.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Nazwa uzytkownika jest wymagana")]
        [Display(Name = "Login")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Email jest wymagany")]
        [EmailAddress(ErrorMessage = "Niepoprawny adres email")]
        [Display(Name = "Email")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Haslo jest wymagana")]
        [Display(Name = "Haslo")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [Display(Name = "Potwierdz haslo")]
        [Compare("Password", ErrorMessage = "Hasla nie sa takie same")]
        public required string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Imię jest wymagane")]
        [Display(Name = "Imie")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        [Display(Name = "Nazwisko")]
        public required string LastName { get; set; }
    }
}