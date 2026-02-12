using APITaskTracker.API.GlobalErrorHandler;
using APITaskTracker.Application.Interfaces;
using APITaskTracker.Application.Mapping;
using APITaskTracker.Application.Services;
using APITaskTracker.Application.Validators;
using APITaskTracker.Domain.Enums;
using APITaskTracker.Infrastructure.Interface;
using APITaskTracker.Infrastructure.Persistence;
using APITaskTracker.Infrastructure.Repository;
using APITaskTracker.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Task = APITaskTracker.Domain.Entities.Tasks.Task;
using TaskStatus = APITaskTracker.Domain.Enums.TaskStatus;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(cfg => { }, typeof(TaskMappingProfile));
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TaskTrackerDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IGenericRepository<Task>, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<TaskCreateDtoValidator>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File(
        path: @"C:\TaskTracker\Logs\log-.txt",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7,
        shared: true)
    .CreateLogger();

builder.Host.UseSerilog();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider
        .GetRequiredService<TaskTrackerDbContext>();

    db.Database.EnsureCreated();

    if (!db.Tasks.Any())
    {
        db.Tasks.AddRange(
            new Task
            {
                Title = "Design Classes",
                Description = "Define classes, their attributes, and methods",
                Status = TaskStatus.Done,
                Priority = TaskPriority.High,
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                DueDate = DateTime.UtcNow.AddDays(1)
            },
            new Task
            {
                Title = "Implement Inheritance",
                Description = "Create derived classes from a base class",
                Status = TaskStatus.InProgress,
                Priority = TaskPriority.High,
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                DueDate = DateTime.UtcNow.AddDays(2)
            }
        );

        db.SaveChanges();
    }
}

app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowFrontend");
app.MapControllers();

app.Run();
