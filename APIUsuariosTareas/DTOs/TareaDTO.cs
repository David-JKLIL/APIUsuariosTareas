using System.ComponentModel.DataAnnotations;

namespace APIUsuariosTareas.DTOs
{
    public class TareaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }

        [RegularExpression("^(Pendiente|EnProceso|Completada|Cancelada)$", ErrorMessage = "Estado no válido.")]
        public string Estado { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string NombreUsuario { get; set; } 
    }

}
