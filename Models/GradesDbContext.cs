using Microsoft.EntityFrameworkCore;

namespace Grades.Models
{
    public class GradesDbContext : DbContext
    {
        public GradesDbContext(DbContextOptions<GradesDbContext> options) : base(options)
        {
        }

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Activity> Activities { get; set; } // Nuevo DbSet para Activity

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configura la relación entre Subject y Activity
            modelBuilder.Entity<Activity>()
                .HasOne(a => a.Subject)
                .WithMany(s => s.Activities)
                .HasForeignKey(a => a.SubjectId);
        }
    }
}