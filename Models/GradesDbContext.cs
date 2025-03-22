using Microsoft.EntityFrameworkCore;

namespace Grades.Models
{
    public class GradesDbContext : DbContext
    {
        public GradesDbContext(DbContextOptions<GradesDbContext> options) : base(options)
        {
        }

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Activity> Activities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configura la relación entre Subject y Activity
            modelBuilder.Entity<Subject>()
                .HasMany(s => s.Activities)
                .WithOne(a => a.Subject)
                .HasForeignKey(a => a.SubjectId)
                .OnDelete(DeleteBehavior.Cascade); // Opcional: Elimina las actividades si se elimina la materia
        }
    }
}