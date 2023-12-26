using Microsoft.EntityFrameworkCore;
using PTN_BackendAssignment.Models;

namespace PTN_BackendAssignment.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }
    }
}
