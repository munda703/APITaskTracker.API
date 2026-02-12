using Microsoft.EntityFrameworkCore;
using Task = APITaskTracker.Domain.Entities.Tasks.Task;

namespace APITaskTracker.Infrastructure.Persistence
{
    public class TaskTrackerDbContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }

        public TaskTrackerDbContext(DbContextOptions<TaskTrackerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(TaskTrackerDbContext).Assembly);
        }
    }
}