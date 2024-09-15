using Microsoft.EntityFrameworkCore;
using WebApi_LS1.Entities;

namespace WebApi_LS1.Data
{
    public class StudentDbContext:DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options)
            :base(options) { }
        public DbSet<Student> Students{ get; set; }
    }
}
