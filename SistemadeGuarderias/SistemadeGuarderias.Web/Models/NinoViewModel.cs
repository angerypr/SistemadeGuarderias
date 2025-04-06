using System.ComponentModel.DataAnnotations;

namespace SistemadeGuarderias.Web.Models
{
    public class NinoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del niño es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido del niño es obligatorio.")]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "La edad del niño es obligatoria.")]
        [Range(1, 18, ErrorMessage = "La edad debe estar entre 1 y 18 años.")]
        public int Edad { get; set; }

        public int TutorId { get; set; }
        public string TutorNombre { get; set; } = string.Empty;  

        public int GuarderiaId { get; set; }
        public string GuarderiaNombre { get; set; } = string.Empty;  

        public string? ActividadNombre { get; set; }  
        public List<AsistenciaViewModel>? Asistencias { get; set; }
        public List<PagoViewModel>? Pagos { get; set; }
        public List<MensajeViewModel>? Mensajes { get; set; }
    }
}
