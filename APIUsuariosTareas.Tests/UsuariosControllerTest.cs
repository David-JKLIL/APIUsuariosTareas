using APIUsuariosTareas.Controllers;
using APIUsuariosTareas.DTOs;
using APIUsuariosTareas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;


namespace APIUsuariosTareas.Tests.Controllers
{
    [TestClass]
    public class UsuariosControllerTest
    {
        [TestMethod]
        public async Task CrearUsuario()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var controller = new UsuariosController(context);

                var nuevoUsuario = new CrearUsuarioDTO
                {
                    Nombre = "David Jaña",
                    Correo = "david.jana@example.com",
                    Contrasena = "password123"
                };

                // Act
                var result = await controller.RegistrarUsuario(nuevoUsuario);

                // Assert
                var createdResult = result as CreatedAtActionResult;
                Assert.IsNotNull(createdResult);

                var usuarioCreado = createdResult.Value as Usuario;
                Assert.IsNotNull(usuarioCreado);
                Assert.AreEqual(nuevoUsuario.Nombre, usuarioCreado.Nombre);
                Assert.AreEqual(nuevoUsuario.Correo, usuarioCreado.correo);

                // Verificar que el usuario está en la base de datos
                var usuarioEnDb = await context.Usuarios.FindAsync(usuarioCreado.Id);
                Assert.IsNotNull(usuarioEnDb);
                Assert.AreEqual(nuevoUsuario.Nombre, usuarioEnDb.Nombre);
                Assert.AreEqual(nuevoUsuario.Correo, usuarioEnDb.correo);
            }
        }
    }
}
