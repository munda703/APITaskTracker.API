using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = APITaskTracker.Domain.Entities.Tasks.Task;

namespace APITaskTracker.Infrastructure.EF_Core_configs
{
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.ToTable("Tasks");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Description)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(t => t.Status)
                .HasConversion<string>();

            builder.Property(t => t.Priority)
                .HasConversion<string>();

            builder.Property(t => t.CreatedAt)
                .IsRequired();

            builder.HasIndex(t => t.DueDate);
        }
    }

}
