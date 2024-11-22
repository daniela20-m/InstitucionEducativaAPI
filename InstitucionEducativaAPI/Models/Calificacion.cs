namespace InstitucionEducativaAPI.Models
{
    public class Calificacion
    {
        public int CalificacionId { get; set; }
        public int EstudianteId { get; set; }
        public int CursoId { get; set; }
        public decimal CalificacionValue { get; set; }

        // Relaciones con las otras entidades
        public Estudiante Estudiante { get; set; }
        public Curso Curso { get; set; }
    }
}
