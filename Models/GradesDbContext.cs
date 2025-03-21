using Microsoft.EntityFrameworkCore;

namespace Grades.Models
{
    public class GradesDbContext : DbContext
    {
        public GradesDbContext(DbContextOptions<GradesDbContext> options) : base(options)
        {
        }

        public DbSet<Subject> Subjects { get; set; }
    }
}