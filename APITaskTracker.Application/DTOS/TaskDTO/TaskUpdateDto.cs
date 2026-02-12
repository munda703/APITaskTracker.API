using APITaskTracker.Domain.Enums;
using TaskStatus = APITaskTracker.Domain.Enums.TaskStatus;

namespace APITaskTracker.Application.DTO.TaskDTO
{
    public class TaskUpdateDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public TaskStatus? Status { get; set; }
        public TaskPriority? Priority { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
