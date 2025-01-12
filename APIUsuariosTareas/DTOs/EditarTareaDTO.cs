using System.ComponentModel.DataAnnotations;

namespace APIUsuariosTareas.DTOs
{
    public class EditarTareaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }

        [RegularExpression("^(Pendiente|EnProceso|Completada|Cancelada)$", ErrorMessage = "Estado no válido.")]
        public string Estado { get; set; }
        public DateTime FechaVencimiento { get; set; }
    }
}
