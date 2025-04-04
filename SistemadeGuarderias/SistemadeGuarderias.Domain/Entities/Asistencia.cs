using SistemadeGuarderias.Domain.Core;

namespace SistemadeGuarderias.Domain.Entities
{
    public class Asistencia : BaseEntity
    {
        public required bool Presente { get; set; }
        public required DateTime Fecha { get; set; }
        public required int NinoId { get; set; }
        public required Nino Nino { get; set; }
        public required int GuarderiaId { get; set; }
        public required Guarderia Guarderia { get; set; }
    }
}
