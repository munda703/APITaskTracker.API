using APITaskTracker.Domain.Enums;
using TaskStatus = APITaskTracker.Domain.Enums.TaskStatus;

namespace APITaskTracker.Application.DTO.TaskDTO
{
    public class TaskReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
