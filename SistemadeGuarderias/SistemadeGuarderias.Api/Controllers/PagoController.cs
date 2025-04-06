using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemadeGuarderias.Application.DTOs;
using SistemadeGuarderias.Domain.Entities;
using SistemadeGuarderias.Infrastructure;

namespace SistemadeGuarderias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagosController : ControllerBase
    {
        private readonly SistemadeGuarderiasDbContext _context;

        public PagosController(SistemadeGuarderiasDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PagoDto>>> GetPagos()
        {
            var pagos = await _context.Pagos
                .Select(p => new PagoDto
                {
                    Id = p.Id,
                    Monto = p.Monto,
                    Fecha = p.Fecha,
                    Pagado = p.Pagado,
                    NinoId = p.NinoId,
                    GuarderiaId = p.GuarderiaId,
                    TutorId = p.TutorId
                })
                .ToListAsync();

            return Ok(pagos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PagoDto>> GetPago(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);

            if (pago == null)
                return NotFound();

            var dto = new PagoDto
            {
                Id = pago.Id,
                Monto = pago.Monto,
                Fecha = pago.Fecha,
                Pagado = pago.Pagado,
                NinoId = pago.NinoId,
                GuarderiaId = pago.GuarderiaId,
                TutorId = pago.TutorId
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<PagoDto>> PostPago(PagoDto dto)
        {
            var nino = await _context.Ninos.FindAsync(dto.NinoId);
            var guarderia = await _context.Guarderias.FindAsync(dto.GuarderiaId);
            var tutor = await _context.Tutores.FindAsync(dto.TutorId);

            if (nino == null || guarderia == null || tutor == null)
                return BadRequest("Niño, guardería o tutor no encontrado.");

            var pago = new Pago
            {
                Monto = dto.Monto,
                Fecha = dto.Fecha,
                Pagado = dto.Pagado,
                NinoId = dto.NinoId,
                GuarderiaId = dto.GuarderiaId,
                TutorId = dto.TutorId,
                Nino = nino,
                Guarderia = guarderia,
                Tutor = tutor
            };

            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();

            dto.Id = pago.Id;

            return CreatedAtAction(nameof(GetPago), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPago(int id, PagoDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El ID proporcionado no coincide con el del DTO.");

            var pago = await _context.Pagos.FindAsync(id);

            if (pago == null)
                return NotFound();

            var nino = await _context.Ninos.FindAsync(dto.NinoId);
            var guarderia = await _context.Guarderias.FindAsync(dto.GuarderiaId);
            var tutor = await _context.Tutores.FindAsync(dto.TutorId);

            if (nino == null || guarderia == null || tutor == null)
                return BadRequest("Niño, guardería o tutor no encontrado.");

            pago.Monto = dto.Monto;
            pago.Fecha = dto.Fecha;
            pago.Pagado = dto.Pagado;
            pago.NinoId = dto.NinoId;
            pago.GuarderiaId = dto.GuarderiaId;
            pago.TutorId = dto.TutorId;
            pago.Nino = nino;
            pago.Guarderia = guarderia;
            pago.Tutor = tutor;

            _context.Pagos.Update(pago);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePago(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null)
                return NotFound();

            _context.Pagos.Remove(pago);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
