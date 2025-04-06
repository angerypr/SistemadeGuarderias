using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemadeGuarderias.Application.DTOs;
using SistemadeGuarderias.Domain.Entities;
using SistemadeGuarderias.Infrastructure;

namespace SistemadeGuarderias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NinosController : ControllerBase
    {
        private readonly SistemadeGuarderiasDbContext _context;

        public NinosController(SistemadeGuarderiasDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NinoDto>>> GetNinos()
        {
            var ninos = await _context.Ninos
                .Select(n => new NinoDto
                {
                    Id = n.Id,
                    Nombre = n.Nombre,
                    Apellido = n.Apellido,
                    Edad = n.Edad,
                    TutorId = n.TutorId,
                    ActividadId = n.ActividadId,
                    GuarderiaId = n.GuarderiaId
                })
                .ToListAsync();

            return Ok(ninos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NinoDto>> GetNino(int id)
        {
            var nino = await _context.Ninos
               .Include(n => n.Tutor)
               .Include(n => n.Guarderia)
               .FirstOrDefaultAsync(n => n.Id == id);

            if (nino == null)
            {
                return NotFound();
            }

            var dto = new NinoDto
            {
                Id = nino.Id,
                Nombre = nino.Nombre,
                Apellido = nino.Apellido,
                Edad = nino.Edad,
                TutorId = nino.TutorId,
                ActividadId = nino.ActividadId,
                GuarderiaId = nino.GuarderiaId
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<NinoDto>> PostNino(NinoDto dto)
        {
            var tutor = await _context.Tutores.FindAsync(dto.TutorId);
            var guarderia = await _context.Guarderias.FindAsync(dto.GuarderiaId);

            if (tutor == null || guarderia == null)
            {
                return BadRequest("Tutor o Guardería no encontrados.");
            }

            Actividad? actividad = null;
            if (dto.ActividadId.HasValue)
            {
                actividad = await _context.Actividades.FindAsync(dto.ActividadId.Value);
                if (actividad == null)
                {
                    return BadRequest("La Actividad con el ID proporcionado no existe.");
                }
            }
            var nino = new Nino
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Edad = dto.Edad,
                TutorId = dto.TutorId,
                ActividadId = dto.ActividadId,
                GuarderiaId = dto.GuarderiaId,
                Tutor = tutor,
                Guarderia = guarderia,
                Actividad = actividad
            };

            _context.Ninos.Add(nino);
            await _context.SaveChangesAsync();

            dto.Id = nino.Id;

            return CreatedAtAction(nameof(GetNino), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutNino(int id, NinoDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID proporcionado no coincide con el del DTO.");
            }

            var nino = await _context.Ninos.FindAsync(id);

            if (nino == null)
            {
                return NotFound();
            }
            var tutor = await _context.Tutores.FindAsync(dto.TutorId);
            var guarderia = await _context.Guarderias.FindAsync(dto.GuarderiaId);

            if (tutor == null || guarderia == null)
            {
                return BadRequest("Tutor o Guardería no encontrados.");
            }

            nino.Nombre = dto.Nombre;
            nino.Apellido = dto.Apellido;
            nino.Edad = dto.Edad;
            nino.TutorId = dto.TutorId;
            nino.ActividadId = dto.ActividadId;
            nino.GuarderiaId = dto.GuarderiaId;
            nino.Tutor = tutor;
            nino.Guarderia = guarderia;

            if (dto.ActividadId.HasValue)
            {
                var actividad = await _context.Actividades.FindAsync(dto.ActividadId.Value);
                if (actividad != null)
                {
                    nino.Actividad = actividad; 
                    nino.ActividadId = dto.ActividadId.Value; 
                }
                else
                {
                    return BadRequest("La Actividad con el ID proporcionado no existe.");
                }
            }
            else
            {
                nino.Actividad = null;
                nino.ActividadId = null; 
            }

            _context.Ninos.Update(nino);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNino(int id)
        {
            var nino = await _context.Ninos.FindAsync(id);
            if (nino == null)
            {
                return NotFound();
            }

            _context.Ninos.Remove(nino);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
