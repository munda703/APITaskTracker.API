using APITaskTracker.Application.DTO.TaskDTO;
using FluentValidation;


namespace APITaskTracker.Application.Validators
{
    public class TaskCreateDtoValidator : AbstractValidator<TaskCreateDto>
    {
        public TaskCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required cannot be empty.")
                .MinimumLength(3)
                .WithMessage("Title must at least 3 characters")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters");
        }
    }
}
