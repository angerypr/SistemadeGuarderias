using System.ComponentModel.DataAnnotations;

namespace SistemadeGuarderias.Application.DTOs
{
    public class TutorDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(100, ErrorMessage = "El apellido no puede tener más de 100 caracteres.")]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Phone(ErrorMessage = "Debe ingresar un número de teléfono válido.")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "La cédula es obligatoria.")]
        [StringLength(20, ErrorMessage = "La cédula no puede tener más de 20 caracteres.")]
        public string Cedula { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Debe ingresar un correo electrónico válido.")]
        public string? CorreoElectronico { get; set; }
    }
}
