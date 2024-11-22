namespace InstitucionEducativaAPI.Models
{
    public class Curso
    {
        public int CursoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        // Relación inversa: Un curso puede tener muchas calificaciones
        public ICollection<Calificacion> Calificaciones { get; set; }
    }
}
