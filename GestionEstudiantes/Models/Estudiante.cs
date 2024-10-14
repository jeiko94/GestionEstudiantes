

namespace GestionEstudiantes.Models
{
    //Representa un estudiante en el sistema
    public class Estudiante
    {
        public int EstuduanteId {  get; set; }
        public  string Nombre { get; set; }
        public string Apellido { get; set; }

        //Relación muchos a muchos con Cursos
        public List<CursoEstudiante> CursosEstudiantes { get; set; }

        public Estudiante()
        {
            CursosEstudiantes = new List<CursoEstudiante>();
        }
    }
}
