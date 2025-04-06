using System.ComponentModel.DataAnnotations;

namespace SistemadeGuarderias.Application.DTOs
{
    public class PagoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que cero.")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "La fecha del pago es obligatoria.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Debe indicar si el pago fue realizado.")]
        public bool Pagado { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un niño.")]
        public int NinoId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una guardería.")]
        public int GuarderiaId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un tutor.")]
        public int TutorId { get; set; }
    }
}
