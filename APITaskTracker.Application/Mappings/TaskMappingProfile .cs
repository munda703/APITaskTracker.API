using APITaskTracker.Application.DTO.TaskDTO;
using AutoMapper;
using Task = APITaskTracker.Domain.Entities.Tasks.Task;

namespace APITaskTracker.Application.Mapping
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            CreateMap<TaskCreateDto, Task>()
                .ForMember(dest => dest.CreatedAt,
                           opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<TaskUpdateDto, Task>()
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Task, TaskReadDto>();
        }
    }
}
