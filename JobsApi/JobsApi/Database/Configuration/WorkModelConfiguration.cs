using JobsApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobsApi.Database.Configuration;

public class WorkModelConfiguration : IEntityTypeConfiguration<WorkModel>
{
    public void Configure(EntityTypeBuilder<WorkModel> builder)
    {
        builder.ToTable("Works");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(255).IsRequired();
        builder.Property(x => x.StartAt).IsRequired().HasDefaultValue(DateTimeOffset.UtcNow);
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.Value).IsRequired();

        builder.HasOne(x => x.User).WithMany(x => x.Works).OnDelete(DeleteBehavior.Restrict);
    }
}