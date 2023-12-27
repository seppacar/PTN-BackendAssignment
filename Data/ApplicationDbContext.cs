using Microsoft.EntityFrameworkCore;
using PTN_BackendAssignment.Models;

namespace PTN_BackendAssignment.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> TasksItems { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set alternate key "Email" or maybe set Unique index?
            modelBuilder.Entity<User>()
                .HasAlternateKey(u => u.Email);

            // AutoInclude Navigation property to owner
            modelBuilder.Entity<TaskItem>()
                .Navigation(t => t.Owner)
                .AutoInclude();
        }
    }
}
