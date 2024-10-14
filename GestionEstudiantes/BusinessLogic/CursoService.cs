using GestionEstudiantes.DataAccess;
using GestionEstudiantes.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionEstudiantes.BusinessLogic
{
    //Logica de negocio para curso
    public class CursoService
    {
        private EscuelaContext _context;

        public CursoService(EscuelaContext context)
        {
            _context = context;
        }

        public List<Curso> ObtenerCursos()
        {
            return _context.Curso.Include(c => c.CursosEstidiantes)
                .ThenInclude(ce => ce.Estudiante)
                .ToList();
        }

        public void AgregarCurso(Curso curso)
        {
            _context.Curso.Add(curso);
            _context.SaveChanges();
        }

        public void EliminarCurso(int cursoId)
        {
            var curso = _context.Curso.Find(cursoId);
            if (curso != null)// si hay datos entonces:
            {
                _context.Curso.Remove(curso);
                _context.SaveChanges();
            }
        }
    }
}
