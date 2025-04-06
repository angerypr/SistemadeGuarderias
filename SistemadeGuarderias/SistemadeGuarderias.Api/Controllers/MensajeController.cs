using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemadeGuarderias.Application.DTOs;
using SistemadeGuarderias.Domain.Entities;
using SistemadeGuarderias.Infrastructure;
using SistemadeGuarderias.Api.Services; 

namespace SistemadeGuarderias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MensajesController : ControllerBase
    {
        private readonly SistemadeGuarderiasDbContext _context;
        private readonly IServicioEmail _servicioEmail;

        public MensajesController(SistemadeGuarderiasDbContext context, IServicioEmail servicioEmail)
        {
            _context = context;
            _servicioEmail = servicioEmail;  
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MensajeDto>>> GetMensajes()
        {
            var mensajes = await _context.Mensajes
                .Select(m => new MensajeDto
                {
                    Id = m.Id,
                    Contenido = m.Contenido,
                    Fecha = m.Fecha,
                    Hora = m.Hora,
                    NinoId = m.NinoId,
                    TutorId = m.TutorId,
                    GuarderiaId = m.GuarderiaId
                }).ToListAsync();

            return Ok(mensajes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MensajeDto>> GetMensaje(int id)
        {
            var mensaje = await _context.Mensajes.FindAsync(id);
            if (mensaje == null)
                return NotFound();

            var dto = new MensajeDto
            {
                Id = mensaje.Id,
                Contenido = mensaje.Contenido,
                Fecha = mensaje.Fecha,
                Hora = mensaje.Hora,
                NinoId = mensaje.NinoId,
                TutorId = mensaje.TutorId,
                GuarderiaId = mensaje.GuarderiaId
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<MensajeDto>> PostMensaje(MensajeDto dto)
        {
            var tutor = await _context.Tutores.FindAsync(dto.TutorId);
            var guarderia = await _context.Guarderias.FindAsync(dto.GuarderiaId);
            var nino = await _context.Ninos.FindAsync(dto.NinoId);

            if (tutor == null || guarderia == null || nino == null)
                return BadRequest("Niño, tutor o guardería no encontrados.");

            var mensaje = new Mensaje
            {
                Contenido = dto.Contenido,
                Fecha = dto.Fecha,
                Hora = dto.Hora,
                NinoId = dto.NinoId,
                TutorId = dto.TutorId,
                GuarderiaId = dto.GuarderiaId,
                Tutor = tutor,
                Nino = nino,
                Guarderia = guarderia
            };

            _context.Mensajes.Add(mensaje);
            await _context.SaveChangesAsync();

            if (!string.IsNullOrWhiteSpace(tutor.CorreoElectronico))
            {
                await _servicioEmail.EnviarEmail(tutor.CorreoElectronico,
                    "Nuevo mensaje de la guardería",
                    $"Se ha enviado el siguiente mensaje sobre {nino.Nombre}:\n\n{mensaje.Contenido}");
            }

            dto.Id = mensaje.Id;
            return CreatedAtAction(nameof(GetMensaje), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMensaje(int id, MensajeDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID proporcionado no coincide con el del DTO.");
            }

            var mensaje = await _context.Mensajes.FindAsync(id);

            if (mensaje == null)
            {
                return NotFound();
            }

            var tutor = await _context.Tutores.FindAsync(dto.TutorId);
            var guarderia = await _context.Guarderias.FindAsync(dto.GuarderiaId);
            var nino = await _context.Ninos.FindAsync(dto.NinoId);

            if (tutor == null || guarderia == null || nino == null)
            {
                return BadRequest("Niño, tutor o guardería no encontrados.");
            }

            mensaje.Contenido = dto.Contenido;
            mensaje.Fecha = dto.Fecha;
            mensaje.Hora = dto.Hora;
            mensaje.NinoId = dto.NinoId;
            mensaje.TutorId = dto.TutorId;
            mensaje.GuarderiaId = dto.GuarderiaId;
            mensaje.Tutor = tutor;
            mensaje.Nino = nino;
            mensaje.Guarderia = guarderia;

            _context.Mensajes.Update(mensaje);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMensaje(int id)
        {
            var mensaje = await _context.Mensajes.FindAsync(id);
            if (mensaje == null)
                return NotFound();

            _context.Mensajes.Remove(mensaje);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
