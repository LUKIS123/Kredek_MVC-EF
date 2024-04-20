using System.ComponentModel.DataAnnotations;

namespace JakubWiesniakLab3.Models
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
        }

        public ProductViewModel(int id, string name, string category, double price, string description, string imageurl)
        {
            Id = id;
            Name = name;
            Category = category;
            Price = price;
            Description = description;
            ImageUrl = imageurl;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa jest wymagana")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Kategoria jest wymagana")]
        [Display(Name = "Kategoria")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Cena jest wymagana")]
        [Display(Name = "Cena")]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        [Required(ErrorMessage = "Opis jest wymagany")]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}