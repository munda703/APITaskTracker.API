using APITaskTracker.Infrastructure.Interface;
using APITaskTracker.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Task = APITaskTracker.Domain.Entities.Tasks.Task;

namespace APITaskTracker.Infrastructure.Repository
{
    public class TaskRepository : GenericRepository<Task>, ITaskRepository
    {
        private const string DueDateDesc = "desc";

        public TaskRepository(TaskTrackerDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Task>> SearchTasksAsync(string? q, string? sort)
        {
            IQueryable<Task> taskItems = _dbSet;

            if (!string.IsNullOrWhiteSpace(q))
            {
                taskItems = taskItems.Where(t =>
                    EF.Functions.Like(t.Title, $"%{q}%") ||
                    (t.Description != null && EF.Functions.Like(t.Description, $"%{q}%"))
                );
            }

            taskItems = sort?.ToLower() switch
            {
                DueDateDesc => taskItems.OrderByDescending(t => t.DueDate),
                _ => taskItems.OrderBy(t => t.DueDate)
            };

            return await taskItems.ToListAsync();
        }
    }
}
