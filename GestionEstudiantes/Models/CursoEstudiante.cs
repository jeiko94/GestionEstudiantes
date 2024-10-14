

namespace GestionEstudiantes.Models
{
    //Representa la relación muchos a muchos entre estudiantes y cursos.
    public class CursoEstudiante
    {
        public int EstudianteId { get; set; }
        public Estudiante Estudiante { get; set; }

        public int cursoId { get; set; }
        public Curso Curso { get; set; }


    }
}
