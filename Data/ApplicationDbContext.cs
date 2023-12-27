using Microsoft.EntityFrameworkCore;
using PTN_BackendAssignment.Models;

namespace PTN_BackendAssignment.Data
{
    /// <summary>
    /// Database context for the application.
    /// </summary>
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

            // AutoInclude Navigation property to owner for TaskItem entity
            modelBuilder.Entity<TaskItem>()
                .Navigation(t => t.Owner)
                .AutoInclude();

            // Convert enums to string for database storage (TaskType and Priority)
            modelBuilder.Entity<TaskItem>()
                .Property(t => t.TaskType)
                .HasConversion(type => type.ToString(),
                 v => (TaskType)Enum.Parse(typeof(TaskType), v));
            modelBuilder.Entity<TaskItem>()
                .Property(t => t.Priority)
                .HasConversion(type => type.ToString(),
                 v => (Priority)Enum.Parse(typeof(Priority), v));
        }
    }
}
