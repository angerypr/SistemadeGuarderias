using SistemadeGuarderias.Domain.Core;

namespace SistemadeGuarderias.Domain.Entities
{
    public class Pago : BaseEntity
    {
        public required decimal Monto { get; set; }
        public required DateTime Fecha { get; set; }
        public required bool Pagado { get; set; }
        public required int NinoId { get; set; }
        public required Nino Nino { get; set; }
        public required int GuarderiaId { get; set; }
        public required Guarderia Guarderia { get; set; }
        public required int TutorId { get; set; }
        public required Tutor Tutor { get; set; }
    }
}
