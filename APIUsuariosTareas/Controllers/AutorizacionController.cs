using APIUsuariosTareas.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace APIUsuariosTareas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorizacionController : Controller
    {
        private readonly AuthService _authService;

        public AutorizacionController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var token = await _authService.AuthenticateAsync(request.Correo, request.Contrasena);

                if (token == null)
                {
                    return Unauthorized(new { Message = "Credenciales inválidas" });
                }

                return Ok(new { token });
            }
            catch
            {
                return Unauthorized(new { Message = "Credenciales inválidas" });
            }
        }

        public class LoginRequest
        {
            public string? Correo { get; set; }
            public string? Contrasena { get; set; }
        }
    }
}
