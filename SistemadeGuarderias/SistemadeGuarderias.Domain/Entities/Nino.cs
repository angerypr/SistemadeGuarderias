using SistemadeGuarderias.Domain.Core;

namespace SistemadeGuarderias.Domain.Entities
{
    public class Nino : BaseEntity
    {
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public int Edad { get; set; }
        public int TutorId { get; set; }
        public required Tutor Tutor { get; set; }
        public int? ActividadId { get; set; }
        public Actividad? Actividad { get; set; }
        public int GuarderiaId { get; set; }
        public required Guarderia Guarderia { get; set; }
        public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
        public ICollection<Asistencia> Asistencias { get; set; } = new List<Asistencia>();
        public ICollection<Mensaje> Mensajes { get; set; } = new List<Mensaje>();

    }
}
