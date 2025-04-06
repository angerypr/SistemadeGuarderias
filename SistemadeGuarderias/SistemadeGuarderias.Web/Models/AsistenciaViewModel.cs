using System.ComponentModel.DataAnnotations;

namespace SistemadeGuarderias.Web.Models
{
    public class AsistenciaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El estado de presencia es obligatorio.")]
        public bool Presente { get; set; }

        public int NinoId { get; set; }
        public string NinoNombre { get; set; } = string.Empty;  
        
        public int GuarderiaId { get; set; }
        public string GuarderiaNombre { get; set; } = string.Empty;  
    }
}
