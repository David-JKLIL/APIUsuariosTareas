using APIUsuariosTareas.DTOs;
using APIUsuariosTareas.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Route("RegistrarUsuario")]
        [AllowAnonymous]
        public async Task<IActionResult> RegistrarUsuario([FromBody] CrearUsuarioDTO request)
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

            //return Ok(new { Message = "Usuario creado exitosamente" });
            return CreatedAtAction(nameof(RegistrarUsuario), new { id = usuario.Id }, usuario);
        }

    }
}
