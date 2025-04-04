using SistemadeGuarderias.Domain.Core;

namespace SistemadeGuarderias.Domain.Entities
{
    public class Mensaje : BaseEntity
    {
        public required string Contenido { get; set; }
        public required DateTime Fecha { get; set; }
        public required TimeSpan Hora { get; set; }
        public int NinoId { get; set; }
        public required Nino Nino { get; set; }
        public int TutorId { get; set; }
        public required Tutor Tutor { get; set; }
        public int GuarderiaId { get; set; }
        public required Guarderia Guarderia { get; set; }

    }

}
