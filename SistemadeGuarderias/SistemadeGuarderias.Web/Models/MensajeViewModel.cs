using System.ComponentModel.DataAnnotations;

namespace SistemadeGuarderias.Web.Models
{
    public class MensajeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El contenido del mensaje es obligatorio.")]
        [StringLength(1000, ErrorMessage = "El contenido no puede tener más de 1000 caracteres.")]
        public string Contenido { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha del mensaje es obligatoria.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "La hora del mensaje es obligatoria.")]
        public TimeSpan Hora { get; set; }

        public int NinoId { get; set; }
        public string NinoNombre { get; set; } = string.Empty;  

        public int TutorId { get; set; }
        public string TutorNombre { get; set; } = string.Empty;  

        public int GuarderiaId { get; set; }
        public string GuarderiaNombre { get; set; } = string.Empty;  
    }
}
