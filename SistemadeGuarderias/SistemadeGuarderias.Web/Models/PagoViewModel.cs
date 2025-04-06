using System.ComponentModel.DataAnnotations;

namespace SistemadeGuarderias.Web.Models
{
    public class PagoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que 0.")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El estado de pago es obligatorio.")]
        public bool Pagado { get; set; }

        public int NinoId { get; set; }
        public string NinoNombre { get; set; } = string.Empty;  

        public int GuarderiaId { get; set; }
        public string GuarderiaNombre { get; set; } = string.Empty;  

        public int TutorId { get; set; }
        public string TutorNombre { get; set; } = string.Empty; 
    }
}
