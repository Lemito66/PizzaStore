using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
        public required string Name { get; set; }

        [MaxLength(200, ErrorMessage = "La descripción no puede superar los 200 caracteres.")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "La categoría debe tener un producto.")]
        public ICollection<Product> Products { get; set; } = new List<Product>(); // Relación uno a muchos con Product
    }
}
