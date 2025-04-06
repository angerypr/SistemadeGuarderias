using System.ComponentModel.DataAnnotations;

namespace SistemadeGuarderias.Application.DTOs
{
    public class NinoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(100, ErrorMessage = "El apellido no puede tener más de 100 caracteres.")]
        public string Apellido { get; set; } = string.Empty;

        [Range(0, 10, ErrorMessage = "La edad debe estar entre 0 y 10 años.")]
        public int Edad { get; set; }

        [Required(ErrorMessage = "El TutorId es obligatorio.")]
        public int TutorId { get; set; }

        public int? ActividadId { get; set; }

        [Required(ErrorMessage = "La Guardería es obligatoria.")]
        public int GuarderiaId { get; set; }
    }
}
