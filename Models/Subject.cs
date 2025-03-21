using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Grades.Models
{
    public class Subject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la materia es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string Name { get; set; }

        // Empieza con una lista vacia de actividades
        public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();
    }
}