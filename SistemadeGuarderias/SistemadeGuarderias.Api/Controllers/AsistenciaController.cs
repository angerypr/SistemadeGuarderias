using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemadeGuarderias.Application.DTOs;
using SistemadeGuarderias.Domain.Entities;
using SistemadeGuarderias.Infrastructure;

namespace SistemadeGuarderias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AsistenciasController : ControllerBase
    {
        private readonly SistemadeGuarderiasDbContext _context;

        public AsistenciasController(SistemadeGuarderiasDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AsistenciaDto>>> GetAsistencias()
        {
            var asistencias = await _context.Asistencias
                .Select(a => new AsistenciaDto
                {
                    Id = a.Id,
                    Presente = a.Presente,
                    Fecha = a.Fecha,
                    NinoId = a.NinoId,
                    GuarderiaId = a.GuarderiaId
                })
                .ToListAsync();

            return Ok(asistencias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AsistenciaDto>> GetAsistencia(int id)
        {
            var asistencia = await _context.Asistencias.FindAsync(id);

            if (asistencia == null)
            {
                return NotFound();
            }

            var dto = new AsistenciaDto
            {
                Id = asistencia.Id,
                Presente = asistencia.Presente,
                Fecha = asistencia.Fecha,
                NinoId = asistencia.NinoId,
                GuarderiaId = asistencia.GuarderiaId
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<AsistenciaDto>> PostAsistencia(AsistenciaDto dto)
        {
            var nino = await _context.Ninos.FindAsync(dto.NinoId);
            var guarderia = await _context.Guarderias.FindAsync(dto.GuarderiaId);

            if (nino == null || guarderia == null)
            {
                return BadRequest("Niño o Guardería no encontrados.");
            }

            var asistencia = new Asistencia
            {
                Presente = dto.Presente,
                Fecha = dto.Fecha,
                NinoId = dto.NinoId,
                GuarderiaId = dto.GuarderiaId,
                Nino = nino,
                Guarderia = guarderia
            };

            _context.Asistencias.Add(asistencia);
            await _context.SaveChangesAsync();

            dto.Id = asistencia.Id;

            return CreatedAtAction(nameof(GetAsistencia), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsistencia(int id, AsistenciaDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID no coincide.");
            }

            var asistencia = await _context.Asistencias.FindAsync(id);
            if (asistencia == null)
            {
                return NotFound();
            }

            asistencia.Presente = dto.Presente;
            asistencia.Fecha = dto.Fecha;
            asistencia.NinoId = dto.NinoId;
            asistencia.GuarderiaId = dto.GuarderiaId;

            _context.Asistencias.Update(asistencia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsistencia(int id)
        {
            var asistencia = await _context.Asistencias.FindAsync(id);
            if (asistencia == null)
            {
                return NotFound();
            }

            _context.Asistencias.Remove(asistencia);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
