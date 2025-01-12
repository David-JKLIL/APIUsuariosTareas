namespace APIUsuariosTareas.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string correo { get; set; }
        public string Contrasena { get; set; }

        //Esto esto es la relacion con tareas - Un usuario puede tener muchas tareas
        public List<Tarea> Tareas { get; set; }

    }
}
