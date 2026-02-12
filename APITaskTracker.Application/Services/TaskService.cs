using APITaskTracker.Application.Interfaces;
using APITaskTracker.Infrastructure.Interface;
using AutoMapper;
using global::APITaskTracker.Application.DTO.TaskDTO;
using Microsoft.Extensions.Logging;

namespace APITaskTracker.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<TaskService> _logger;

        public TaskService(
            ITaskRepository taskRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<TaskService> logger)
        {
            _taskRepository = taskRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<TaskReadDto>> GetAllAsync()
        {
            try
            {
                var tasks = await _taskRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<TaskReadDto>>(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all tasks");
                throw;
            }
        }

        public async Task<TaskReadDto?> GetByIdAsync(int id)
        {
            try
            {
                var task = await _taskRepository.GetByIdAsync(id);
                return task == null ? null : _mapper.Map<TaskReadDto>(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving task with Id {TaskId}", id);
                throw;
            }
        }

        public async Task<IEnumerable<TaskReadDto>> SearchAsync(string? q, string? sort)
        {
            try
            {
                var tasks = await _taskRepository.SearchTasksAsync(q, sort);
                return _mapper.Map<IEnumerable<TaskReadDto>>(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching tasks. Query: {Query}, Sort: {Sort}", q, sort);
                throw;
            }
        }

        public async Task<TaskReadDto> CreateAsync(TaskCreateDto dto)
        {
            try
            {
                var task = _mapper.Map<Domain.Entities.Tasks.Task>(dto);

                await _unitOfWork.BeginTransactionAsync();
                await _taskRepository.AddAsync(task);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<TaskReadDto>(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating task {@TaskCreateDto}", dto);
                throw;
            }
        }

        public async Task<TaskReadDto> UpdateAsync(int id, TaskUpdateDto dto)
        {
            try
            {
                var task = await _taskRepository.GetByIdAsync(id);
                if (task == null)
                {
                    _logger.LogWarning("Task with Id {TaskId} not found for update", id);
                    throw new KeyNotFoundException("Task not found");
                }

                _mapper.Map(dto, task);

                await _unitOfWork.BeginTransactionAsync();
                var res = await _taskRepository.Update(task);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<TaskReadDto>(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating task with Id {TaskId}. Payload {@TaskUpdateDto}", id, dto);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var task = await _taskRepository.GetByIdAsync(id);
                if (task == null)
                {
                    _logger.LogWarning("Task with Id {TaskId} not found for deletion", id);
                    throw new KeyNotFoundException("Task not found");
                }

                await _unitOfWork.BeginTransactionAsync();
                _taskRepository.Delete(task);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting task with Id {TaskId}", id);
                throw;
            }
        }
    }
}
