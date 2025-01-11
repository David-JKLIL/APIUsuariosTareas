using Microsoft.AspNetCore.Mvc;
using APIUsuariosTareas.Models;
using Microsoft.EntityFrameworkCore;

namespace APIUsuariosTareas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : Controller
    {

        private readonly AppDbContext _context;

        public TareasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Tarea>> CrearTarea(Tarea tarea)
        {

            //VALIDACION DE USUARIO
            var usuario = await _context.Usuarios.FindAsync(tarea.UsuarioId);
            if (usuario == null)
            {
                return BadRequest("El usuario no existe.");
            }

            //GUARDAR TAREA
            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CrearTarea), new { id = tarea.Id }, tarea);

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarea>>> ObtenerTareas(string? estado, DateTime? fechaVencimiento)
        {

            var query = _context.Tareas.AsQueryable();
            if (!string.IsNullOrEmpty(estado))
            {
                query = query.Where(x => x.Estado == estado);
            }
            if (fechaVencimiento == null)
            {
                query = query.Where(x => x.FechaVencimiento == fechaVencimiento);
            }

            var tareas = await query.ToListAsync();

            return Ok(tareas);


        }

    }
}
