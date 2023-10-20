
using ApiEquipo.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiEquipo.Controllers
{
    [Route("api/equipo")]
    [ApiController]
    public class EquipoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EquipoController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult<Equipo>> CrearEmpleado(Equipo empleado)
        {
            if (empleado.JefeId.HasValue)
            {
                var jefeExistente = await _context.Equipos.FindAsync(empleado.JefeId);
                if (jefeExistente == null)
                {
                    return BadRequest("El jefe especificado no existe.");
                }
            }

            _context.Equipos.Add(empleado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmpleado", new { id = empleado.Id }, empleado);
        }




        [HttpGet]
        public async Task<ActionResult<List<Equipo>>> Get()
        {
            return await _context.Equipos.ToListAsync();
        }


        [HttpGet("{id}", Name = "GetEquipo")]
        public async Task<ActionResult<Equipo>> GetEmpleado(int id)
        {
            var modelo = await _context.Equipos.FindAsync(id);

            if (modelo == null)
            {
                return NotFound();
            }

            return modelo;
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEmpleado(int id, Equipo modelo)
        {
            try
            {
                var equipo = await _context.Equipos.FindAsync(id);

                if (modelo == null)
                {
                    return NotFound("Empleado no encontrado");
                }

                // Actualiza los campos del empleado con los valores proporcionados en el modelo
                modelo.Nombre = modelo.Nombre;
                modelo.Telefono = modelo.Telefono;
                modelo.Email = modelo.Email;
                modelo.Rol = modelo.Rol;

                if (modelo.JefeId.HasValue)
                {
                    var jefeExistente = await _context.Equipos.FindAsync(modelo.JefeId);
                    if (jefeExistente == null)
                    {
                        return BadRequest("El jefe especificado no existe.");
                    }
                    modelo.JefeId = modelo.JefeId;
                }

                _context.Update(modelo);
                await _context.SaveChangesAsync();

                return Ok("Empleado actualizado con éxito");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Equipo>> DeleteEmpleado(int id)
        {
            var equipo = await _context.Equipos.FindAsync(id);
            if (equipo == null)
            {
                return NotFound();
            }

            _context.Equipos.Remove(equipo);
            await _context.SaveChangesAsync();

            return equipo;
        }

    }
}
