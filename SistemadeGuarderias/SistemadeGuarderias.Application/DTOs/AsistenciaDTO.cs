namespace SistemadeGuarderias.Application.DTOs
{
    public class AsistenciaDto
    {
        public int Id { get; set; }
        public bool Presente { get; set; }
        public DateTime Fecha { get; set; }
        public int NinoId { get; set; }
        public int GuarderiaId { get; set; }
    }
}
