

namespace GestionEstudiantes.Models
{
    public class Curso
    {
        public int CursoId { get; set; }
        public string Nombre { get; set; }

        //Relación muchos a muchos con Estudiantes
        public List<CursoEstudiante> CursosEstidiantes { get; set; }

        public Curso()
        {
            CursosEstidiantes = new List<CursoEstudiante>();
        }
    }
}
