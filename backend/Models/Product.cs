using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
        public required string Name { get; set; }
        [Required(ErrorMessage = "La descripción es requerida")]
        [MaxLength(500, ErrorMessage = "La descripción no puede tener más de 500 caracteres")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0.01, 10000, ErrorMessage = "El precio debe ser mayor que 0 y menor que 10000.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Es obligatorio especificar si el producto está disponible.")]
        public bool IsAvailable { get; set; }
        [Required(ErrorMessage = "La imagen es obligatoria.")]
        public string Image { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;


        [Required(ErrorMessage = "La categoría es obligatoria.")]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!; // Relación muchos a uno con Category
    }
}
