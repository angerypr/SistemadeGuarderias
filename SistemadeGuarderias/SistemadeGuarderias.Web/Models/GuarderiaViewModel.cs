using System.ComponentModel.DataAnnotations;

namespace SistemadeGuarderias.Web.Models
{
    public class GuarderiaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        public string Direccion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Phone(ErrorMessage = "El formato del teléfono no es válido.")]
        public string Telefono { get; set; } = string.Empty;

        public List<NinoViewModel>? Ninos { get; set; }
        public List<AsistenciaViewModel>? Asistencias { get; set; }
        public List<ActividadViewModel>? Actividades { get; set; }
        public List<PagoViewModel>? Pagos { get; set; }
        public List<MensajeViewModel>? Mensajes { get; set; }
    }
}
