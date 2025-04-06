using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemadeGuarderias.Application.DTOs;
using SistemadeGuarderias.Domain.Entities;
using SistemadeGuarderias.Infrastructure;

namespace SistemadeGuarderias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuarderiasController : ControllerBase
    {
        private readonly SistemadeGuarderiasDbContext _context;

        public GuarderiasController(SistemadeGuarderiasDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuarderiaDTO>>> GetGuarderias()
        {
            var guarderias = await _context.Guarderias
                .Select(g => new GuarderiaDTO
                {
                    Id = g.Id,
                    Nombre = g.Nombre,
                    Direccion = g.Direccion,
                    Telefono = g.Telefono
                })
                .ToListAsync();

            return Ok(guarderias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GuarderiaDTO>> GetGuarderia(int id)
        {
            var guarderia = await _context.Guarderias.FindAsync(id);

            if (guarderia == null)
                return NotFound();

            var dto = new GuarderiaDTO
            {
                Id = guarderia.Id,
                Nombre = guarderia.Nombre,
                Direccion = guarderia.Direccion,
                Telefono = guarderia.Telefono
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<GuarderiaDTO>> PostGuarderia(GuarderiaDTO dto)
        {
            var guarderia = new Guarderia
            {
                Nombre = dto.Nombre,
                Direccion = dto.Direccion,
                Telefono = dto.Telefono
            };

            _context.Guarderias.Add(guarderia);
            await _context.SaveChangesAsync();

            dto.Id = guarderia.Id;

            return CreatedAtAction(nameof(GetGuarderia), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGuarderia(int id, GuarderiaDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("El ID proporcionado no coincide con el del DTO.");

            var guarderia = await _context.Guarderias.FindAsync(id);

            if (guarderia == null)
                return NotFound();

            guarderia.Nombre = dto.Nombre;
            guarderia.Direccion = dto.Direccion;
            guarderia.Telefono = dto.Telefono;

            _context.Guarderias.Update(guarderia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuarderia(int id)
        {
            var guarderia = await _context.Guarderias.FindAsync(id);

            if (guarderia == null)
                return NotFound();

            _context.Guarderias.Remove(guarderia);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
