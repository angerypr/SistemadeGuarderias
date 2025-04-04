using SistemadeGuarderias.Domain.Core;

namespace SistemadeGuarderias.Domain.Entities
{
    public class Actividad : BaseEntity
    {
        public required string Descripcion { get; set; }
        public required DateTime Fecha { get; set; }
        public required TimeSpan Hora { get; set; }
        public ICollection<Nino> Ninos { get; set; } = new List<Nino>();
        public int GuarderiaId { get; set; }
        public required Guarderia Guarderia { get; set; }
    }

}
