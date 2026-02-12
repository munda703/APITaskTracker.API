using APITaskTracker.API.Controllers;
using APITaskTracker.Application.DTO.TaskDTO;
using APITaskTracker.Application.Interfaces;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;

namespace APITaskTracker.Tests.Controllers
{
    public class TasksControllerTests
    {
        private readonly ITaskService _fakeService;
        private readonly TasksController _controller;

        public TasksControllerTests()
        {
            _fakeService = A.Fake<ITaskService>();
            _controller = new TasksController(_fakeService);
        }

        [Fact]
        public async Task GetAll_ReturnsOk_WithTasks()
        {
            var tasks = new List<TaskReadDto>
            {
                new() { Id = 1, Title = "Task 1" },
                new() { Id = 2, Title = "Task 2" }
            };

            A.CallTo(() => _fakeService.SearchAsync(null, null))
                .Returns(tasks);

            var result = await _controller.GetAll(null, null);

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var value = Assert.IsAssignableFrom<IEnumerable<TaskReadDto>>(ok.Value);
            Assert.Equal(2, value.Count());
        }

        [Fact]
        public async Task GetAll_ReturnsEmptyList_WhenNoTasks()
        {
            A.CallTo(() => _fakeService.SearchAsync("none", null))
                .Returns(Enumerable.Empty<TaskReadDto>());

            var result = await _controller.GetAll("none", null);

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var value = Assert.IsAssignableFrom<IEnumerable<TaskReadDto>>(ok.Value);
            Assert.Empty(value);
        }

        [Fact]
        public async Task GetById_ReturnsOk_WhenTaskExists()
        {
            var dto = new TaskReadDto { Id = 1, Title = "Task" };

            A.CallTo(() => _fakeService.GetByIdAsync(1))
                .Returns(dto);

            var result = await _controller.GetById(1);

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var value = Assert.IsType<TaskReadDto>(ok.Value);
            Assert.Equal(1, value.Id);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenTaskDoesNotExist()
        {
            A.CallTo(() => _fakeService.GetByIdAsync(99))
                .Returns((TaskReadDto?)null);

            var result = await _controller.GetById(99);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsOk_WithCreatedTask()
        {
            var createDto = new TaskCreateDto { Title = "New Task" };
            var readDto = new TaskReadDto { Id = 1, Title = "New Task" };

            A.CallTo(() => _fakeService.CreateAsync(createDto))
                .Returns(readDto);

            var result = await _controller.Create(createDto);

            var ok = Assert.IsType<CreatedAtActionResult>(result.Result);
            var value = Assert.IsType<TaskReadDto>(ok.Value);
            Assert.Equal("New Task", value.Title);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenSuccessful()
        {
            var updateDto = new TaskUpdateDto { Title = "Updated" };
            A.CallTo(() => _fakeService.UpdateAsync(1, updateDto))
                .ReturnsLazily(() => Task
                .FromResult(new TaskReadDto{Title = "Updated"}));

            var result = await _controller.UpdateTask(1, updateDto);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var value = Assert.IsType<TaskReadDto>(okResult.Value);
            Assert.Equal("Updated", value.Title);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenSuccessful()
        {
            A.CallTo(() => _fakeService.DeleteAsync(1))
                .Returns(Task.CompletedTask);

            var result = await _controller.Delete(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAll_Throws_WhenServiceThrows()
        {
            A.CallTo(() => _fakeService.SearchAsync(null, null))
                .Throws(new Exception("Boom"));

            await Assert.ThrowsAsync<Exception>(() => _controller.GetAll(null, null));
        }
    }
}