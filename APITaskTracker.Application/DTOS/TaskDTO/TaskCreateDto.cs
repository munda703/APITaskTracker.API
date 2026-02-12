using APITaskTracker.Domain.Enums;
namespace APITaskTracker.Application.DTO.TaskDTO
{
   public class TaskCreateDto
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public TaskPriority Priority { get; set; } = TaskPriority.Low;
        public DateTime? DueDate { get; set; }
    }
}
