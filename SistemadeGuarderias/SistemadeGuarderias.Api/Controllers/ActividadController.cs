using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemadeGuarderias.Application.DTOs;
using SistemadeGuarderias.Domain.Entities;
using SistemadeGuarderias.Infrastructure;

namespace SistemadeGuarderias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActividadesController : ControllerBase
    {
        private readonly SistemadeGuarderiasDbContext _context;

        public ActividadesController(SistemadeGuarderiasDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActividadDTO>>> GetActividades()
        {
            var actividades = await _context.Actividades
                .Select(a => new ActividadDTO
                {
                    Id = a.Id,
                    Descripcion = a.Descripcion,
                    Fecha = a.Fecha,
                    Hora = a.Hora,
                    GuarderiaId = a.GuarderiaId
                })
                .ToListAsync();

            return Ok(actividades);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActividadDTO>> GetActividad(int id)
        {
            var actividad = await _context.Actividades.FindAsync(id);

            if (actividad == null)
                return NotFound();

            var dto = new ActividadDTO
            {
                Id = actividad.Id,
                Descripcion = actividad.Descripcion,
                Fecha = actividad.Fecha,
                Hora = actividad.Hora,
                GuarderiaId = actividad.GuarderiaId
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<ActividadDTO>> PostActividad(ActividadDTO dto)
        {
            var guarderia = await _context.Guarderias.FindAsync(dto.GuarderiaId);
            if (guarderia == null)
                return BadRequest("La guardería no existe.");

            var actividad = new Actividad
            {
                Descripcion = dto.Descripcion,
                Fecha = dto.Fecha,
                Hora = dto.Hora,
                GuarderiaId = dto.GuarderiaId,
                Guarderia = guarderia
            };

            _context.Actividades.Add(actividad);
            await _context.SaveChangesAsync();

            dto.Id = actividad.Id;

            return CreatedAtAction(nameof(GetActividad), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutActividad(int id, ActividadDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("El ID proporcionado no coincide con el del DTO.");

            var actividad = await _context.Actividades.FindAsync(id);

            if (actividad == null)
                return NotFound();

            var guarderia = await _context.Guarderias.FindAsync(dto.GuarderiaId);
            if (guarderia == null)
                return BadRequest("La guardería no existe.");

            actividad.Descripcion = dto.Descripcion;
            actividad.Fecha = dto.Fecha;
            actividad.Hora = dto.Hora;
            actividad.GuarderiaId = dto.GuarderiaId;

            _context.Actividades.Update(actividad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActividad(int id)
        {
            var actividad = await _context.Actividades.FindAsync(id);

            if (actividad == null)
                return NotFound();

            _context.Actividades.Remove(actividad);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
