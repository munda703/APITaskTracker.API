using APITaskTracker.Application.DTO.TaskDTO;

namespace APITaskTracker.Application.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskReadDto>> GetAllAsync();
        Task<TaskReadDto?> GetByIdAsync(int id);
        Task<IEnumerable<TaskReadDto>> SearchAsync(string? q, string? sort);
        Task<TaskReadDto> CreateAsync(TaskCreateDto dto);
        Task<TaskReadDto> UpdateAsync(int id, TaskUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
