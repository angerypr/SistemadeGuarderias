using System.ComponentModel.DataAnnotations;

namespace SistemadeGuarderias.Application.DTOs
{
    public class ActividadDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(300, ErrorMessage = "La descripción no puede tener más de 300 caracteres.")]
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "La hora es obligatoria.")]
        public TimeSpan Hora { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una guardería.")]
        public int GuarderiaId { get; set; }
    }
}
