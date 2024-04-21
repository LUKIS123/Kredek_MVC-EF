using System.ComponentModel.DataAnnotations;

namespace JakubWiesniakLab3.Models
{
    public class BeginOrderViewModel
    {
        [Required(ErrorMessage = "Telefon jest wymagany")]
        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Moasto jest wymagane")]
        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Required(ErrorMessage = "Adres jest wymagana")]
        [Display(Name = "Adres")]
        public string Address { get; set; }

        public int ProductId { get; set; }
    }
}