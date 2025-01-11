using APIUsuariosTareas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIUsuariosTareas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : Controller
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody] CrearUsuarioRequest request)
        {
            // Verificar si el correo ya existe
            var usuarioExistente = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.correo == request.Correo);

            if (usuarioExistente != null)
            {
                return BadRequest("El correo ya está registrado.");
            }

            // Hashear la contraseña antes de guardarla
            string contrasenaHasheada = BCrypt.Net.BCrypt.HashPassword(request.Contrasena);

            // Crear el nuevo usuario con la contraseña hasheada
            var usuario = new Usuario
            {
                Nombre = request.Nombre,
                correo = request.Correo,
                Contrasena = contrasenaHasheada
            };

            // Guardar el usuario en la base de datos
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Usuario creado exitosamente" });
        }

        public class CrearUsuarioRequest
        {
            public string Nombre { get; set; }
            public string Correo { get; set; }
            public string Contrasena { get; set; }
        }
    }
}
