using System.ComponentModel.DataAnnotations;

namespace SistemadeGuarderias.Application.DTOs
{
    public class GuarderiaDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(200, ErrorMessage = "El nombre no puede tener más de 200 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [StringLength(300, ErrorMessage = "La dirección no puede tener más de 300 caracteres.")]
        public string Direccion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [StringLength(15, ErrorMessage = "El teléfono no puede tener más de 15 caracteres.")]
        public string Telefono { get; set; } = string.Empty;
    }
}
