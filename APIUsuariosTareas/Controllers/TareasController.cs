using Microsoft.AspNetCore.Mvc;
using APIUsuariosTareas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using APIUsuariosTareas.DTOs;
using System.Security.Claims;

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
        [Route("CrearTarea")]
        [Authorize]
        public async Task<ActionResult<Tarea>> CrearTarea(CrearTareaDTO tareaDTO)
        {
            // Validación del estado
            var estadosValidos = new[] { "Pendiente", "EnProceso", "Completada", "Cancelada" };
            if (!estadosValidos.Contains(tareaDTO.Estado))
            {
                return BadRequest(new { Message = "Estado no válido." });
            }

            var idClaim = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(idClaim))
            {
                return Unauthorized(new { Message = "El usuario no está autenticado o falta el ID en el token." });
            }

            var usuarioId = int.Parse(idClaim);

            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
            {
                return Unauthorized("Usuario no autorizado.");
            }

            var tarea = new Tarea
            {
                Titulo = tareaDTO.Titulo,
                Descripcion = tareaDTO.Descripcion,
                Estado = tareaDTO.Estado,
                FechaVencimiento = tareaDTO.FechaVencimiento,
                UsuarioId = usuarioId
            };

            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();

            //return Ok(new { Message = "Tarea creada" });
            return CreatedAtAction(nameof(CrearTarea), new { id = tarea.Id }, tareaDTO);
        }



        [HttpGet]
        [Route("ObtenerTareas")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Tarea>>> ObtenerTareas(string? estado, DateTime? fechaVencimiento, int pagina = 1, int tamañoPagina = 10)
        {
            var idClaim = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(idClaim))
            {
                return Unauthorized(new { Message = "El usuario no está autenticado" });
            }

            var usuarioId = int.Parse(idClaim);

            var query = _context.Tareas.Where(t => t.UsuarioId == usuarioId);

            if (!string.IsNullOrEmpty(estado))
            {
                query = query.Where(x => x.Estado == estado);
            }

            if (fechaVencimiento.HasValue)
            {
                query = query.Where(x => x.FechaVencimiento.Date == fechaVencimiento.Value.Date);
            }

            var totalTareas = await query.CountAsync();
            var tareas = await query
                                .Skip((pagina - 1) * tamañoPagina)
                                .Take(tamañoPagina)
                                .ToListAsync();

            var tareasDTO = tareas.Select(t => new TareaDTO
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descripcion = t.Descripcion,
                Estado = t.Estado,
                FechaVencimiento = t.FechaVencimiento,
                NombreUsuario = _context.Usuarios.Find(t.UsuarioId).Nombre ?? "Desconocido"
            });

            var respuesta = new
            {
                TotalTareas = totalTareas,
                TareasPorPagina = tareas.Count,
                PaginaActual = pagina,
                TotalPaginas = (int)Math.Ceiling(totalTareas / (double)tamañoPagina),
                Tareas = tareasDTO
            };

            return Ok(respuesta);
        }



        [HttpPut]
        [Route("EditarTarea")]
        [Authorize]
        public async Task<ActionResult<Tarea>> EditarTarea(EditarTareaDTO tareaDTO)
        {
            var idClaim = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(idClaim))
            {
                return Unauthorized(new { Message = "El usuario no está autenticado" });
            }

            var usuarioId = int.Parse(idClaim);

            var tarea = await _context.Tareas.FindAsync(tareaDTO.Id);

            if (tarea == null)
            {
                return NotFound(new { Message = "Tarea no encontrada" });
            }

            if (tarea.UsuarioId != usuarioId)
            {
                return Unauthorized(new { Message = "No tienes permiso para editar esta tarea" });
            }

            tarea.Titulo = tareaDTO.Titulo;
            tarea.Descripcion = tareaDTO.Descripcion;
            tarea.Estado = tareaDTO.Estado;
            tarea.FechaVencimiento = tareaDTO.FechaVencimiento;

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Tarea editada" });
        }


        [HttpDelete]
        [Route("EliminarTarea")]
        [Authorize]
        public async Task<ActionResult<Tarea>> EliminarTarea(int id)
        {
            var idClaim = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(idClaim))
            {
                return Unauthorized(new { Message = "El usuario no está autenticado" });
            }

            var usuarioId = int.Parse(idClaim);

            var tarea = await _context.Tareas.FindAsync(id);

            if (tarea == null)
            {
                return NotFound(new { Message = "Tarea no encontrada" });
            }

            if (tarea.UsuarioId != usuarioId)
            {
                return Unauthorized(new { Message = "No tienes permiso para eliminar esta tarea" });
            }

            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Tarea eliminada" });
        }



    }
}
