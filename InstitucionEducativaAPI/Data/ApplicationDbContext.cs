using InstitucionEducativaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InstitucionEducativaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Curso> Cursos { get; set; }

        public DbSet<Calificacion> Calificaciones { get; set; }
    }
}
