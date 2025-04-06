using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemadeGuarderias.Application.DTOs;
using SistemadeGuarderias.Domain.Entities;
using SistemadeGuarderias.Infrastructure;

namespace SistemadeGuarderias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TutoresController : ControllerBase
    {
        private readonly SistemadeGuarderiasDbContext _context;

        public TutoresController(SistemadeGuarderiasDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TutorDto>>> GetTutores()
        {
            var tutores = await _context.Tutores
                .Select(t => new TutorDto
                {
                    Id = t.Id,
                    Nombre = t.Nombre,
                    Apellido = t.Apellido,
                    Telefono = t.Telefono,
                    Cedula = t.Cedula,
                    CorreoElectronico = t.CorreoElectronico
                })
                .ToListAsync();

            return Ok(tutores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TutorDto>> GetTutor(int id)
        {
            var tutor = await _context.Tutores.FindAsync(id);

            if (tutor == null)
                return NotFound();

            var dto = new TutorDto
            {
                Id = tutor.Id,
                Nombre = tutor.Nombre,
                Apellido = tutor.Apellido,
                Telefono = tutor.Telefono,
                Cedula = tutor.Cedula,
                CorreoElectronico = tutor.CorreoElectronico
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<TutorDto>> PostTutor(TutorDto dto)
        {
            var tutor = new Tutor
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Telefono = dto.Telefono,
                Cedula = dto.Cedula,
                CorreoElectronico = dto.CorreoElectronico,
                Ninos = new List<Nino>() // Importante para evitar nulls
            };

            _context.Tutores.Add(tutor);
            await _context.SaveChangesAsync();

            dto.Id = tutor.Id;
            return CreatedAtAction(nameof(GetTutor), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTutor(int id, TutorDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El ID proporcionado no coincide.");

            var tutor = await _context.Tutores.FindAsync(id);
            if (tutor == null)
                return NotFound();

            tutor.Nombre = dto.Nombre;
            tutor.Apellido = dto.Apellido;
            tutor.Telefono = dto.Telefono;
            tutor.Cedula = dto.Cedula;
            tutor.CorreoElectronico = dto.CorreoElectronico;

            _context.Tutores.Update(tutor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTutor(int id)
        {
            var tutor = await _context.Tutores.FindAsync(id);
            if (tutor == null)
                return NotFound();

            _context.Tutores.Remove(tutor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
