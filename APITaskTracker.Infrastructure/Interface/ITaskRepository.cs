using Task = APITaskTracker.Domain.Entities.Tasks.Task;

namespace APITaskTracker.Infrastructure.Interface
{
    public interface ITaskRepository : IGenericRepository<Task>
    {
        Task<IEnumerable<Task>> SearchTasksAsync(string? q, string? sort);
    }
}
