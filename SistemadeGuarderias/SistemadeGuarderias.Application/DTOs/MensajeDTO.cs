using System.ComponentModel.DataAnnotations;

namespace SistemadeGuarderias.Application.DTOs
{
    public class MensajeDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El contenido es obligatorio.")]
        public string Contenido { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "La hora es obligatoria.")]
        public TimeSpan Hora { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un niño.")]
        public int NinoId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un tutor.")]
        public int TutorId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una guardería.")]
        public int GuarderiaId { get; set; }
    }
}
