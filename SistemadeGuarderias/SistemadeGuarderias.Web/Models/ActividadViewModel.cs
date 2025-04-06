using System.ComponentModel.DataAnnotations;

namespace SistemadeGuarderias.Web.Models
{
    public class ActividadViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "La hora es obligatoria.")]
        public TimeSpan Hora { get; set; }

        public int GuarderiaId { get; set; }
        public string GuarderiaNombre { get; set; } = string.Empty;  

        public ICollection<int> NinosIds { get; set; } = new List<int>();  

        public ICollection<string> NinosNombres { get; set; } = new List<string>();
    }
}
