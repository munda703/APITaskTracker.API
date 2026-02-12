using APITaskTracker.Application.DTO.TaskDTO;
using APITaskTracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APITaskTracker.API.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskReadDto>>> GetAll(
            [FromQuery] string? q,
            [FromQuery] string? sort)
        {
            var tasks = await _taskService.SearchAsync(q, sort);
            return Ok(tasks);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TaskReadDto>> GetById(int id)
        {
            var task = await _taskService.GetByIdAsync(id);
            if (task == null)
                return NotFound();

            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskReadDto>> Create([FromBody] TaskCreateDto dto)
        {
            var createdTask = await _taskService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TaskReadDto>> UpdateTask(int id,TaskUpdateDto dto)
        {
            var updatedTask = await _taskService.UpdateAsync(id, dto);
            return Ok(updatedTask);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _taskService.DeleteAsync(id);
            return NoContent();
        }
    }
}