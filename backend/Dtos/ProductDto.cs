using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Dtos
{
    //[Table("Products")]
    public class ProductDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "La descripción es requerida")]
        [MaxLength(500, ErrorMessage = "La descripción no puede tener más de 500 caracteres")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0.01, 10000, ErrorMessage = "El precio debe ser mayor que 0 y menor que 10000.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Es obligatorio especificar si el producto está disponible.")]
        public bool IsAvailable { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria.")]
        public int CategoryId { get; set; }
    }

}
