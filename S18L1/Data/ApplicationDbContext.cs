using Microsoft.EntityFrameworkCore;
using S18L1.Models;

namespace S18L1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Student> Stundents { get; set; }
    }
}
