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
            if (ModelState.IsValid)
            {
                _context.Subjects.Add(subject);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
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
    }
}