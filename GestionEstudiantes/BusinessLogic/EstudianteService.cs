
using GestionEstudiantes.DataAccess;
using GestionEstudiantes.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionEstudiantes.BusinessLogic
{
    public class EstudianteService
    {
        private EscuelaContext _context;

        public EstudianteService(EscuelaContext context)
        {
            _context = context;
        }

        public List<Estudiante> ObtenerEstudiantes()
        {
            return _context.Estudites.Include(e => e.CursosEstudiantes)
                .ThenInclude(ce => ce.Curso)
                .ToList();
        }

        public void AgregarEstudiante(Estudiante estudiante)
        {
            _context.Estudites.Add(estudiante);
            _context.SaveChanges();
        }

        public void EliminarEstudiantes(int estudianteId)
        {
            var estudiante = _context.Estudites.Find(estudianteId);
            if (estudiante != null )//si hay datos
            {
                _context.Estudites.Remove(estudiante);
                _context.SaveChanges();
            }
        }
    }
}
