using la_mia_pizzeria_crud_mvc.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace la_mia_pizzeria_crud_mvc.Models
{
    public class Pizza
    {
        [Key] public int Id { get; set; }
        [Required(ErrorMessage = "il nome è obbligatorio")]
        [MaxLength(100, ErrorMessage = "La lunghezza massima è di 100 caratteri")]
        public string Name { get; set; }
        [Pizza5Words]
        [Required(ErrorMessage = "La descrizione è obbligatoria")] 
        [Column(TypeName = "text")] 
        public string Description { get; set; }
        [Url(ErrorMessage = "Devi inserire un link valido")]
        [MaxLength(500, ErrorMessage = "Il link non può essere più lungo di 500 caratteri")]
        public string Image { get; set; }
        public float Price { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public List<Ingredient>? Ingredients { get; set; }
        public Pizza() { }
        public Pizza(string name, string description, string image, float price)
        {
            this.Name = name;
            this.Description = description;
            this.Image = image;
            this.Price = price;
        }
    }
}
