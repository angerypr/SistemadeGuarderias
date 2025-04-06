using System.ComponentModel.DataAnnotations;

namespace SistemadeGuarderias.Web.Models
{
    public class TutorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Phone(ErrorMessage = "El formato del teléfono no es válido.")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "La cédula es obligatoria.")]
        public string Cedula { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        public string? CorreoElectronico { get; set; }
        
        public List<NinoViewModel>? Ninos { get; set; }

        public List<MensajeViewModel>? Mensajes { get; set; }

        public List<PagoViewModel>? Pagos { get; set; }
    }
}
