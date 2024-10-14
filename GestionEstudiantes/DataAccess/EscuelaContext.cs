//Explicación:
//DbContext: EscuelaContext hereda de DbContext y representa el contexto de la base de datos.
//DbSet: Representa las tablas en la base de datos.
//Fluent API: Se utiliza en OnModelCreating para configurar las relaciones y las claves primarias.

using GestionEstudiantes.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionEstudiantes.DataAccess
{
    //Contexto de la base de datos para la escuela
    public class EscuelaContext : DbContext
    {
        public EscuelaContext(DbContextOptions<EscuelaContext> options)
            : base(options)
        {
        }

        public DbSet<Estudiante> Estudites { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<CursoEstudiante> CursosEstudiantes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configurar la clave primaria de estudiante
            modelBuilder.Entity<Estudiante>()
                .HasKey(e => e.EstuduanteId);

            //Configurar la clave primaria de curso
            modelBuilder.Entity<Curso>()
                .HasKey(c => c.CursoId);

            //Configurar la clave primaria compuesta de CursoEstudiante
            modelBuilder.Entity<CursoEstudiante>()
                .HasKey(ce => new { ce.EstudianteId, ce.cursoId });

            //Configurar la relación muchos a muchos entre estudiante y curso
            modelBuilder.Entity<CursoEstudiante>()
                .HasOne(ce => ce.Estudiante)
                .WithMany(e => e.CursosEstudiantes)
                .HasForeignKey(ce => ce.EstudianteId);

            modelBuilder.Entity<CursoEstudiante>()
                .HasOne(ce => ce.Curso)
                .WithMany(c => c.CursosEstidiantes)
                .HasForeignKey(ce => ce.cursoId);

            base.OnModelCreating(modelBuilder);
               
        }

    }
}
