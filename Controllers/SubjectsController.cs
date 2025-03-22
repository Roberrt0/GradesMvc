using Microsoft.AspNetCore.Mvc;
using Grades.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Grades.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly GradesDbContext _context;

        public SubjectsController(GradesDbContext context)
        {
            _context = context;
        }

        // GET: Subjects
        public IActionResult Index()
        {
            var subjects = _context.Subjects.ToList();
            return View(subjects);
        }

        // GET: Subjects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subjects/Create
        [HttpPost]
        public IActionResult Create(Subject subject)
        {
            Console.WriteLine("Iniciando acción Create...");

            if (ModelState.IsValid)
            {
                Console.WriteLine("El modelo es válido.");

                try
                {
                    Console.WriteLine("Agregando la materia al contexto...");
                    _context.Subjects.Add(subject);

                    Console.WriteLine("Guardando cambios en la base de datos...");
                    _context.SaveChanges();

                    Console.WriteLine("Materia creada exitosamente. Redirigiendo a la lista de materias...");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al guardar la materia: {ex.Message}");
                    // Puedes agregar más detalles del error si es necesario
                    Console.WriteLine($"StackTrace: {ex.StackTrace}");
                }
            }
            else
            {
                Console.WriteLine("El modelo no es válido. Errores de validación:");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"- {error.ErrorMessage}");
                }
            }

            Console.WriteLine("Mostrando la vista con errores...");
            return View(subject);
        }

        // GET: Subjects/Edit/5
        public IActionResult Edit(int id)
        {
            var subject = _context.Subjects.Find(id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }

        // POST: Subjects/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, Subject subject)
        {
            if (id != subject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Subjects.Update(subject);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        // GET: Subjects/Delete/5
        public IActionResult Delete(int id)
        {
            var subject = _context.Subjects.Find(id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var subject = _context.Subjects.Find(id);
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        // mostrar las actividades de una materia
        public IActionResult Details(int id)
        {
            var subject = _context.Subjects
                .Include(s => s.Activities) // Incluye las actividades relacionadas
                .FirstOrDefault(s => s.Id == id);

            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // Acción para mostrar el formulario de creación de una nueva actividad
        public IActionResult CreateActivity(int subjectId)
        {
            var subject = _context.Subjects.Find(subjectId);
            if (subject == null)
            {
                return NotFound();
            }

            ViewBag.SubjectId = subjectId;
            return View();
        }

        // Acción para procesar el formulario de creación de una nueva actividad
        [HttpPost]
        public IActionResult CreateActivity(int subjectId, Activity activity)
        {
            Console.WriteLine("Iniciando acción CreateActivity...");

            if (ModelState.IsValid)
            {
                Console.WriteLine("El modelo es válido.");

                try
                {
                    // Asigna el SubjectId a la actividad
                    activity.SubjectId = subjectId;

                    activity.Subject = _context.Subjects.Find(subjectId);

                    Console.WriteLine("Agregando la actividad al contexto...");
                    _context.Activities.Add(activity);

                    Console.WriteLine("Guardando cambios en la base de datos...");
                    _context.SaveChanges();

                    Console.WriteLine("Actividad creada exitosamente. Redirigiendo a los detalles de la materia...");
                    return RedirectToAction("Details", new { id = subjectId });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al guardar la actividad: {ex.Message}");
                    Console.WriteLine($"StackTrace: {ex.StackTrace}");
                }
            }
            else
            {
                Console.WriteLine("El modelo no es válido. Errores de validación:");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"- {error.ErrorMessage}");
                }
            }

            Console.WriteLine("Mostrando la vista con errores...");
            ViewBag.SubjectId = subjectId;
            return View(activity);
        }

        // MODIFICAR Y ELIMINAR ACTIVIDADES:
        // GET: Mostrar el formulario de edición
        [HttpGet]
        public IActionResult EditActivity(int id)
        {
            var activity = _context.Activities.Find(id);
            if (activity == null)
            {
                return NotFound();
            }
            return View(activity);
        }

        // POST: Guardar cambios
        [HttpPost]
        public IActionResult EditActivity(int id, Activity activity)
        {
            if (id != activity.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Subject");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activity);
                    _context.SaveChanges();
                    return RedirectToAction("Details", new { id = activity.SubjectId });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al actualizar la actividad: {ex.Message}");
                }
            }

            return View(activity);
        }

        // GET: Confirmar eliminación
        [HttpGet]
        public IActionResult DeleteActivity(int id)
        {
            var activity = _context.Activities.Find(id);
            if (activity == null)
            {
                return NotFound();
            }
            return View(activity);
        }

        // POST: Ejecutar eliminación
        [HttpPost, ActionName("DeleteActivity")]
        public IActionResult DeleteActivityConfirmed(int id)
        {
            var activity = _context.Activities.Find(id);
            if (activity == null)
            {
                return NotFound();
            }

            _context.Activities.Remove(activity);
            _context.SaveChanges();

            return RedirectToAction("Details", new { id = activity.SubjectId });
        }

    }
}