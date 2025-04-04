using SistemadeGuarderias.Domain.Core;

namespace SistemadeGuarderias.Domain.Entities
{
    public class Tutor : BaseEntity
    {
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Telefono { get; set; }
        public required string Cedula { get; set; }
        public string? CorreoElectronico { get; set; }
        public required ICollection<Nino> Ninos { get; set; } = new List<Nino>();
        public ICollection<Mensaje> Mensajes { get; set; } = new List<Mensaje>();
        public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    }
}
