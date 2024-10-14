//Explicación:

//Menú Principal: Permite navegar entre la gestión de estudiantes, cursos y matriculación.
//Gestión de Estudiantes: Permite listar, agregar y eliminar estudiantes.
//Gestión de Cursos: Permite listar, agregar y eliminar cursos.
//Matrícula: Permite matricular estudiantes en cursos.
//Validaciones Básicas: Verifica si el estudiante y el curso existen antes de matricular.

using GestionEstudiantes.BusinessLogic;
using GestionEstudiantes.DataAccess;
using GestionEstudiantes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GestionEstudiantes.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            //Configurar la conexión a la base de datos
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<EscuelaContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            //Crear el contexto y los servicios
            using (var context = new EscuelaContext(optionsBuilder.Options))
            {
                EstudianteService estudianteService = new EstudianteService(context);
                CursoService cursoService = new CursoService(context);
                MatriculaService matriculaService = new MatriculaService(context);

                bool salir = false;

                while (!salir)
                {
                    Console.WriteLine("\n--- Menú Principal ---");
                    Console.WriteLine("1. Gestionar Estudiantes");
                    Console.WriteLine("2. Gestionar Cursos");
                    Console.WriteLine("3. Matricular Estudiante en Curso");
                    Console.WriteLine("4. Salir");
                    Console.Write("Seleccione una opción: ");

                    int opcion = int.Parse(Console.ReadLine());

                    switch(opcion)
                    {
                        case 1:
                            MenuEstudiantes(estudianteService);
                            break;
                        case 2:
                            MenuCursos(cursoService);
                            break;
                        case 3:
                            MatricularEstudianteEnCurso(estudianteService, cursoService, matriculaService);
                            break;
                        case 4:
                            salir = true;
                            break;
                        default:
                            Console.WriteLine("Opción invalida.");
                            break;
                    }
                }
            }
        }

        //Métodos o funciones para gestionar estudiantes, cursos y matriculas...
        static void MenuEstudiantes(EstudianteService estudianteService)
        {
            bool volver = false;

            while (!volver)
            {
                Console.WriteLine("\n--- Gestión de Estudiantes ---");
                Console.WriteLine("1. Listar Estudiantes");
                Console.WriteLine("2. Agregar Estudiante");
                Console.WriteLine("3. Eliminar Estudiante");
                Console.WriteLine("4. Volver al Menú Principal");
                Console.Write("Seleccione una opción: ");

                int opcion = int.Parse(Console.ReadLine());

                switch(opcion)
                {
                    case 1:
                        ListarEstudiantes(estudianteService);
                        break;
                    case 2:
                        AgregarEstudiante(estudianteService);
                        break;
                    case 3:
                        EliminarEstudiante(estudianteService);
                        break;
                    case 4:
                        volver = true;
                        break;
                    default:
                        Console.WriteLine("Opción invalida.");
                        break;
                }
            }
        }

        static void ListarEstudiantes(EstudianteService estudianteService)
        {
            var estudiantes = estudianteService.ObtenerEstudiantes();
            Console.WriteLine("\n--- Lista de estudiantes ---");
            foreach(var estudiante in estudiantes)
            {
                Console.WriteLine($"ID: {estudiante.EstuduanteId}, Nombre: {estudiante.Nombre}, Apellido: {estudiante.Apellido}");
                if (estudiante.CursosEstudiantes.Any())
                {
                    Console.WriteLine("Cursos: ");
                    foreach (var ce in estudiante.CursosEstudiantes)
                    {
                        Console.WriteLine($"\t- {ce.Curso.Nombre}");
                    }
                }
            }
        }

        static void AgregarEstudiante(EstudianteService estudianteService)
        {
            Console.WriteLine("\nIngresa el nombre del estudiante: ");
            string nombre = Console.ReadLine();

            Console.WriteLine("\nIngresa el apellido del estudiante: ");
            string apellido = Console.ReadLine();

            Estudiante nuevoEstudiante = new Estudiante{ Nombre = nombre, Apellido = apellido};
            estudianteService.AgregarEstudiante (nuevoEstudiante);

            Console.WriteLine("Estudiante agregado exitosamente");
        }

        static void EliminarEstudiante(EstudianteService estudianteService)
        {
            Console.WriteLine("\nIngresa el ID del estudiante a eliminar: ");
            int id = int.Parse(Console.ReadLine());

            estudianteService.EliminarEstudiantes(id);
            Console.WriteLine("Estudiante eliminado con exito: ");
        }

        static void MenuCursos(CursoService cursoService)
        {
            bool volver = false;

            while (!volver)
            {
                Console.WriteLine("\n--- Gestión de Cursos ---");
                Console.WriteLine("1. Listar cursos");
                Console.WriteLine("2. Agregar curso");
                Console.WriteLine("3. Eliminar curso");
                Console.WriteLine("4. Volver al Menú Principal");
                Console.Write("Seleccione una opción: ");

                int opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        ListarCursos(cursoService);
                        break;
                    case 2:
                        AgregarCurso(cursoService);
                        break;
                    case 3:
                        EliminarCurso(cursoService);
                        break;
                    case 4:
                        volver = true;
                        break;
                    default:
                        Console.WriteLine("Opción invalida.");
                        break;
                }
            }
        }

        static void ListarCursos(CursoService cursoService)
        {
            var cursos = cursoService.ObtenerCursos();
            Console.WriteLine("\n--- Lista de cursos ---");
            foreach (var curso in cursos)
            {
                Console.WriteLine($"ID: {curso.CursoId}, Nombre: {curso.Nombre}");
                if (curso.CursosEstidiantes.Any())
                {
                    Console.WriteLine("Estudiantes matriculados: ");
                    foreach(var ce in curso.CursosEstidiantes)
                    {
                        Console.WriteLine($"\t- {ce.Estudiante.Nombre} {ce.Estudiante.Apellido}");
                    }
                }
            }
        }

        static void AgregarCurso(CursoService cursoService)
        {
            Console.WriteLine("\nIngrese el nombre del curso: ");
            string nombre = Console.ReadLine();

            Curso nuevoCurso = new Curso { Nombre = nombre};
            cursoService.AgregarCurso(nuevoCurso);

            Console.WriteLine("Curso agregado exitosamente.");
        }

        static void EliminarCurso(CursoService cursoService)
        {
            Console.WriteLine("\nIngresa el ID del curso a eliminar: ");
            int id = int.Parse(Console.ReadLine());

            cursoService.EliminarCurso(id);
        
            Console.WriteLine("Curso eliminado exitosamente.");
        }

        static void MatricularEstudianteEnCurso(EstudianteService estudianteService, CursoService cursoService, MatriculaService matriculaService)
        {
            Console.WriteLine("\nIngresa el ID del estudiante: ");
            int estudianteId = int.Parse(Console.ReadLine());

            Console.WriteLine("\nIngresa el ID del curso: ");
            int cursoId = int.Parse(Console.ReadLine());

            //Varificar si el estudiante y el curso existen
            var estudiante = estudianteService.ObtenerEstudiantes().FirstOrDefault(e => e.EstuduanteId == estudianteId);
            var curso = cursoService.ObtenerCursos().FirstOrDefault(c => c.CursoId == cursoId);

            if (estudiante == null)
            {
                Console.WriteLine("El estudiante no existe.");
                return;
            }

            if (curso == null)
            {
                Console.WriteLine("El curso no existe.");
                return;
            }

            matriculaService.MatricularEstudiante(estudianteId, cursoId);
            Console.WriteLine("Estudiante matriculado exitosamente en el curso.");

        } 
    }
}