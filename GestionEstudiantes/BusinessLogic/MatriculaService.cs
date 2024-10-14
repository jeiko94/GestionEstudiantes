

using GestionEstudiantes.DataAccess;
using GestionEstudiantes.Models;

namespace GestionEstudiantes.BusinessLogic
{
    //Logica de negocio para la matricula de estudiantes en cursos.
    public class MatriculaService
    {
        private EscuelaContext _context;

        public MatriculaService(EscuelaContext context)
        {
            _context = context;
        }

        public void MatricularEstudiante(int estudianteId, int cursoId)
        {
            var matriculaExistente = _context.CursosEstudiantes.Find(estudianteId, cursoId);
            if (matriculaExistente == null)//No hay registros
            {
                var cursoEstudiante = new CursoEstudiante
                {
                    EstudianteId = estudianteId,
                    cursoId = cursoId
                };
                _context.CursosEstudiantes.Add(cursoEstudiante);
                _context.SaveChanges();
            }
        }

        public void DesmatricularEstudiante(int estudianteId, int cursoId)
        {
            var cursoEstudiante = _context.CursosEstudiantes.Find(estudianteId, cursoId);
            if(cursoEstudiante != null)//ya se encuentran registros
            {
                _context.CursosEstudiantes.Remove(cursoEstudiante);
                _context.SaveChanges();
            }
        }
    }
}
