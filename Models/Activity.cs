using System;
using System.ComponentModel.DataAnnotations;

namespace Grades.Models
{
    public class Activity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El tipo de actividad es obligatorio.")]
        [StringLength(50, ErrorMessage = "El tipo no puede tener más de 50 caracteres.")]
        public string Type { get; set; } // Tarea, Actividad, Prueba, etc.

        [Required(ErrorMessage = "La calificación es obligatoria.")]
        [Range(0, 100, ErrorMessage = "La calificación debe estar entre 0 y 100.")]
        public decimal Grade { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime Date { get; set; }

        [StringLength(500, ErrorMessage = "Los comentarios no pueden tener más de 500 caracteres.")]
        public string Comments { get; set; } // Campo opcional para comentarios

        // Relación con Subject (no es requerida)
        public int SubjectId { get; set; } // Clave foránea para la relación con Subject
        public virtual Subject? Subject { get; set; }
    }
}